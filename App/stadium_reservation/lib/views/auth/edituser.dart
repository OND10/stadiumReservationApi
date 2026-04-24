import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';

class Edituser extends StatefulWidget {
  final String userId;
  Edituser({Key? key, required this.userId}) : super(key: key);

  @override
  _Edituser createState() => _Edituser();
}

class _Edituser extends State<Edituser> {
  final UserController userController = Get.put(UserController());

  @override
  void initState() {
    super.initState();
    userController.getUserById(widget.userId); // Fetch user details
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
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
                        controller: userController.nameController,
                        hint: 'Name',
                        icon: Icons.person,
                        keyboardType: TextInputType.text,
                      ),
                      SizedBox(height: 10),
                      _buildTextFormField(
                        controller: userController.phoneNumberController,
                        hint: 'Phone Number',
                        icon: Icons.phone,
                        keyboardType: TextInputType.phone,
                      ),
                      SizedBox(height: 20),
                      // Buttons
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                        children: [
                          ElevatedButton(
                            onPressed: () {
                              // Trigger update operation
                              userController.updateUser(widget.userId);
                            },
                            child: Text('Update'),
                          ),
                          ElevatedButton(
                            onPressed: () {
                              Get.back(); // Navigate back
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
    );
  }

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
