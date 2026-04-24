import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';
import 'package:stadium_reservation/views/auth/edituser.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

class UserProfilePage extends StatefulWidget {
  final String userId;
  UserProfilePage({Key? key, required this.userId}) : super(key: key);

  @override
  _UserProfilePageState createState() => _UserProfilePageState();
}

class _UserProfilePageState extends State<UserProfilePage> {
  final UserController userController = Get.put(UserController());
  bool _isExpanded = false;

  @override
  void initState() {
    super.initState();
    userController.getUserById(widget.userId); // Fetch user data
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      backgroundColor: Colors.black, // Ensure the background remains dark
      body: Obx(() {
        if (userController.isLoading.value) {
          return Center(child: CircularProgressIndicator(color: Colors.white));
        } else if (userController.user.value.name == null) {
          return Center(
              child: Text('No user details found',
                  style: TextStyle(color: Colors.white)));
        } else {
          return Stack(
            children: [
              Positioned.fill(
                child: Image.asset(
                  'assets/images/ground.png',
                  fit: BoxFit.cover,
                ),
              ),
              Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    GestureDetector(
                      onTap: () {
                        setState(() {
                          _isExpanded = !_isExpanded; // Toggle expanded state
                        });
                      },
                      child: AnimatedContainer(
                        duration: Duration(milliseconds: 500),
                        curve: Curves.easeInOut,
                        width: _isExpanded ? 300 : 120,
                        height: _isExpanded ? 300 : 120,
                        decoration: BoxDecoration(
                          shape: BoxShape.circle,
                          image: DecorationImage(
                            image: userController.user.value.imageUrl != null
                                ? NetworkImage(
                                    userController.user.value.imageUrl!)
                                : AssetImage('assets/images/appm.png')
                                    as ImageProvider,
                            fit: BoxFit.cover,
                          ),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.5),
                              blurRadius: 20,
                              offset: Offset(0, 10),
                            ),
                          ],
                        ),
                      ),
                    ),
                    SizedBox(height: 20),
                    AnimatedOpacity(
                      opacity: _isExpanded ? 0 : 1,
                      duration: Duration(milliseconds: 500),
                      child: _isExpanded
                          ? Container() // Hide details when expanded
                          : ProfileDetails(
                              userController: userController,
                              userId: widget.userId), // Pass userId here
                    ),
                  ],
                ),
              ),
            ],
          );
        }
      }),
    );
  }
}

class ProfileDetails extends StatelessWidget {
  final UserController userController;
  final String userId;

  ProfileDetails({required this.userController, required this.userId});

  @override
  Widget build(BuildContext context) {
    final user = userController.user.value;

    return Container(
      width: 300,
      padding: EdgeInsets.all(16.0),
      decoration: BoxDecoration(
        color: Colors.black.withOpacity(0.6),
        borderRadius: BorderRadius.circular(15),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.4),
            blurRadius: 10,
            offset: Offset(0, 5),
          ),
        ],
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              IconButton(
                onPressed: () {
                  Get.to(() => Edituser(userId: userId)); // Pass correct userId
                },
                icon: Icon(Icons.edit),
                color: AppColors.whiteColor,
              ),
            ],
          ),
          Row(
            children: [
              Icon(Icons.person, color: AppColors.whiteColor),
              SizedBox(width: 60),
              Text(
                "@" + (user.name ?? 'Unknown User'),
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ],
          ),
          SizedBox(height: 10),
          Row(
            children: [
              Icon(Icons.email, color: AppColors.whiteColor),
              SizedBox(width: 15),
              Text(
                user.email ?? 'Email not available',
                style: TextStyle(color: Colors.white70, fontSize: 16),
              ),
            ],
          ),
          SizedBox(height: 10),
          Row(
            children: [
              Icon(Icons.phone_android, color: AppColors.whiteColor),
              SizedBox(width: 60),
              Text(
                user.phoneNumber ?? 'Phone not available',
                style: TextStyle(color: Colors.white70, fontSize: 16),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
