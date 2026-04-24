import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/stadium_controller.dart';
import 'package:stadium_reservation/views/components/drawer.dart';
import 'package:stadium_reservation/views/home_page.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

class StadiumFormPage extends StatelessWidget {
  final StadiumController stadiumController = Get.put(StadiumController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: AppColors.mainColor,
      ),
      body: Stack(
        children: [
          // Background image
          Container(
            decoration: BoxDecoration(
              image: DecorationImage(
                image: AssetImage('assets/images/ground.png'),
                fit: BoxFit.cover,
              ),
            ),
          ),
          Center(
            child: SingleChildScrollView(
              child: Container(
                padding: EdgeInsets.all(16.0),
                child: Form(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      _buildTextFormField(
                        controller: stadiumController.nameController,
                        hint: 'Name',
                        icon: Icons.sports_soccer,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.priceInHourController,
                        hint: 'Price in Hour',
                        icon: Icons.attach_money,
                        keyboardType: TextInputType.number,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.typeController,
                        hint: 'Type',
                        icon: Icons.category,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.noOfPlayersController,
                        hint: 'No of Players',
                        icon: Icons.group,
                      ),
                      SizedBox(height: 10),
                      // Dropdown for selecting Stadium Center
                      Obx(
                        () => DropdownButtonFormField<String>(
                          decoration: InputDecoration(
                            prefixIcon:
                                Icon(Icons.stadium, color: Colors.white),
                            hintStyle: TextStyle(color: Colors.white),
                            filled: true,
                            fillColor: Colors.white.withOpacity(0.2),
                            border: OutlineInputBorder(
                              borderRadius: BorderRadius.circular(30.0),
                              borderSide: BorderSide.none,
                            ),
                          ),
                          style: const TextStyle(color: Colors.white),
                          dropdownColor: Color.fromARGB(255, 21, 71, 51),
                          value: stadiumController
                                  .selectedStadiumCenterId.value.isEmpty
                              ? null
                              : stadiumController.selectedStadiumCenterId.value,
                          items: stadiumController.stadiumCenters.map((center) {
                            return DropdownMenuItem<String>(
                              value: center['id'],
                              child: Text(center['name']),
                            );
                          }).toList(),
                          hint: Text(
                            "Stadium Center",
                            style: TextStyle(color: Colors.white),
                          ),
                          onChanged: (value) {
                            stadiumController.selectedStadiumCenterId.value =
                                value!;
                          },
                        ),
                      ),
                      SizedBox(height: 20),
                      // Buttons
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                        children: [
                          ElevatedButton(
                            onPressed: stadiumController.addStadium,
                            child: Text('Send'),
                          ),
                          ElevatedButton(
                            onPressed: () {
                              Get.to(() => HomePage());
                            },
                            child: Text('Cancel'),
                          ),
                        ],
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
      // drawer: AppDrawer(),
    );
  }

  // Helper method to create a styled TextFormField
  Widget _buildTextFormField({
    required TextEditingController controller,
    required String hint,
    required IconData icon,
    TextInputType keyboardType = TextInputType.text,
  }) {
    return TextFormField(
      controller: controller,
      decoration: InputDecoration(
        prefixIcon: Icon(icon, color: Colors.white),
        hintText: hint,
        hintStyle: TextStyle(color: Colors.white),
        filled: true,
        fillColor: Colors.white.withOpacity(0.2),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0),
          borderSide: BorderSide.none,
        ),
      ),
      style: TextStyle(color: Colors.white),
      keyboardType: keyboardType,
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Please enter $hint';
        }
        return null;
      },
    );
  }
}
