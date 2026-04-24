import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/stadium_controller.dart';
import 'package:stadium_reservation/views/components/drawer.dart';
import 'package:stadium_reservation/views/home_page.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

class StadiumCenterForm extends StatelessWidget {
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
                        icon: Icons.center_focus_strong,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.priceInHourController,
                        hint: 'Phone Number',
                        icon: Icons.phone,
                        keyboardType: TextInputType.number,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.typeController,
                        hint: 'Location',
                        icon: Icons.location_city,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: stadiumController.noOfPlayersController,
                        hint: 'OwnedBy',
                        icon: Icons.supervised_user_circle,
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
                              Get.to(()=>HomePage());
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
      drawer: AppDrawer(),
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
