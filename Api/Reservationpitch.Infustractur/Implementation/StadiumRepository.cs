using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Data;
using Reservationpitch.Infustractur.Database;

namespace Reservationpitch.Infustractur.Implementation
{
    public class StadiumRepository : GenericRepository<Stadium>,IStadiumRepository
    {
        public StadiumRepository(ApplicationDbContext context):base(context)
        {
            
        }


        public bool IsReferencedInOtherTable(int id, int status)
        {
            try
            {
                var entityType = _context.Model.FindEntityType(typeof(Stadium));

                if (entityType == null)
                    throw new Exception("Entity type 'Provider' is not found in the model.");

                // الحصول على جميع الجداول التي تحتوي على مفتاح أجنبي يشير إلى Provider
                var foreignKeyTables = _context.Model.GetEntityTypes()
                    .Where(e => e.GetForeignKeys().Any(fk => fk.PrincipalEntityType == entityType))
                    .ToList();

                if (!foreignKeyTables.Any())
                    return false;

                // التكرار عبر الجداول المرتبطة
                foreach (var table in foreignKeyTables)
                {
                    var tableName = table.GetTableName(); // اسم الجدول المرتبط بـ Provider
                    var foreignKey = table.GetForeignKeys()
                        .First(fk => fk.PrincipalEntityType == entityType)
                        .Properties.First().Name; // اسم المفتاح الأجنبي

                    var typeoftable = table.GetType();
                    // بناء الاستعلام للتحقق مما إذا كان Provider مستخدمًا في الجدول المرتبط
                    var sql = $"SELECT * FROM [{tableName}] WHERE [{foreignKey}] = @id";
                    //var qury = _context.Set(table.ClrType).FromSqlRaw(sql, new SqlParameter("@id", id)).ToList();

                    // إذا تم العثور على بيانات مرتبطة بـ Provider
                    //if (qury != null && qury.Any())
                    //{
                    //    foreach (var row in qury)
                    //    {
                    //        // تحديث السجل إذا كان مرتبطًا
                    //        row.StatusId = (int)status;
                    //        _db.Update(row);
                    //    }
                    //    _db.SaveChanges();
                    //    return true; // وجد ارتباط
                    //}
                }

                return false; // لا يوجد ارتباط مع أي جدول
            }
            catch (Exception ex)
            {
                // تسجيل الاستثناء إذا لزم الأمر
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync(CancellationToken cancellationToken)
        {

            //var query =  _context.Database.SqlQuery<object>($"Select * from Stadiums");

            string modelname = "Stadium";


            IsReferencedInOtherTable(1, 1);

            Type type = Type.GetType($"Reservationpitch.Domain.Entities.{modelname}");

            Console.WriteLine(type);


            var result = await _context.Stadiums.ToListAsync();
            if(result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<Stadium>();
        }

        public async Task<IEnumerable<Stadium>>GetAllStadiumByCenterId(Guid centerId, CancellationToken cancellationToken)
        {
            var result = await _context.Stadiums.Where(s=> s.stadiumCenterId == centerId).ToListAsync();

            if(result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<Stadium>();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Stadium> UpdateAsync(Stadium entity, CancellationToken cancellationToken)
        {
            var findId = await _context.Stadiums.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (findId != null)
            {
                // Update the blogpost
                _context.Entry(findId).CurrentValues.SetValues(entity);
               
                await _context.SaveChangesAsync();
                return entity;
            }
            else
            {
                return null;
            }
        }
    }
}
