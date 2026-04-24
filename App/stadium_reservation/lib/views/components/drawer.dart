import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:stadium_reservation/controllers/login_controller.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';
import 'package:stadium_reservation/views/aboutus.dart';
import 'package:stadium_reservation/views/auth/userlistpage.dart';
import 'package:stadium_reservation/views/auth/userprofile.dart';
import 'package:stadium_reservation/views/home_page.dart';
import 'package:stadium_reservation/views/notificationform.dart';
import 'package:stadium_reservation/views/stadiumcenterform.dart';
import 'package:stadium_reservation/views/style/color_app.dart';
import 'package:stadium_reservation/views/workdayform.dart';

class AppDrawer extends StatefulWidget {
  const AppDrawer({Key? key}) : super(key: key);

  @override
  _AppDrawerState createState() => _AppDrawerState();
}

class _AppDrawerState extends State<AppDrawer> {
  final LoginController loginController = Get.put(LoginController());
  final UserController userController = Get.put(UserController());

  String? userId;
  String? userName;
  String? imageUrl;
  List<String>? roles = []; // Initialize roles as an empty list

  @override
  void initState() {
    super.initState();
    _loadUserId(); // Load userId and roles from SharedPreferences when widget initializes.
  }

  Future<void> _loadUserId() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    setState(() {
      userId =
          prefs.getString('userId'); // Fetch userId from shared preferences
      userName = prefs.getString('userName'); // Fetch userName
      imageUrl = prefs.getString('userImage'); // Fetch userImage
      String? rolesString = prefs.getString('roles'); // Fetch roles
      if (rolesString != null) {
        roles = List<String>.from(
            jsonDecode(rolesString)); // Convert roles from JSON string to list

        print('-----------------------------------');
        print("Roles: $roles");
        print('-----------------------------------');
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return ClipPath(
      clipper: OvalRightBorderClipper(),
      child: Drawer(
        child: Container(
          padding: const EdgeInsets.only(left: 16.0, right: 40),
          decoration: const BoxDecoration(
            color: Color.fromARGB(255, 3, 24, 19),
            boxShadow: [BoxShadow(color: Colors.black45)],
          ),
          width: 300,
          child: SafeArea(
            child: SingleChildScrollView(
              child: Column(
                children: <Widget>[
                  // Logout button
                  Container(
                    alignment: Alignment.centerRight,
                    child: IconButton(
                      icon: Icon(Icons.power_settings_new,
                          color: AppColors.whiteColor),
                      onPressed: () async {
                        loginController.logout(); // Logout action
                      },
                    ),
                  ),
                  // User Avatar and Name
                  Container(
                    height: 90,
                    alignment: Alignment.center,
                    decoration: BoxDecoration(
                      shape: BoxShape.circle,
                      gradient: LinearGradient(
                          colors: [Colors.pink, Colors.deepPurple]),
                    ),
                    child: CircleAvatar(
                      radius: 40,
                      backgroundImage: imageUrl != null && imageUrl!.isNotEmpty
                          ? NetworkImage(imageUrl!)
                          : const AssetImage('assets/images/default_avatar.png')
                              as ImageProvider,
                    ),
                  ),
                  const SizedBox(height: 5.0),
                  Text(
                    userName ?? 'Guest User',
                    style: const TextStyle(
                        color: AppColors.whiteColor, fontSize: 18.0),
                  ),
                  const SizedBox(height: 10.0),

                  // Drawer Menu Items
                  // Home
                  Row(
                    children: [
                      IconButton(
                        icon: Icon(Icons.home, color: AppColors.whiteColor),
                        onPressed: () {
                          // Navigator.of(context).pushNamedAndRemoveUntil(
                          // '/', (Route<dynamic> route) => false);
                          Get.to(HomePage());
                        },
                      ),
                      const Text('Home',
                          style: TextStyle(color: AppColors.whiteColor)),
                    ],
                  ),
                  const SizedBox(height: 10.0),

                  // Profile
                  Row(
                    children: [
                      IconButton(
                        icon: Icon(Icons.person, color: AppColors.whiteColor),
                        onPressed: userId != null
                            ? () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (context) =>
                                        UserProfilePage(userId: userId!),
                                  ),
                                );
                              }
                            : null,
                      ),
                      const Text('Profile',
                          style: TextStyle(color: AppColors.whiteColor)),
                    ],
                  ),
                  const SizedBox(height: 10.0),

                  // Centers (Visible only for admin roles)
                  if (roles != null &&
                      roles!.contains(
                          'Admin')) // Check if the user has an 'Admin' role
                    Row(
                      children: [
                        IconButton(
                          icon: Icon(Icons.admin_panel_settings,
                              color: AppColors.whiteColor),
                          onPressed: userId != null
                              ? () {
                                  Get.to(() => Workdayform());
                                }
                              : null,
                        ),
                        const Text('Centers',
                            style: TextStyle(color: AppColors.whiteColor)),
                      ],
                    ),

                  const SizedBox(height: 10.0),

                  if (roles != null && roles!.contains('Admin'))
                    Row(
                      children: [
                        IconButton(
                          icon: Icon(Icons.private_connectivity,
                              color: AppColors.whiteColor),
                          onPressed: userId != null
                              ? () => Get.to(() => UserListPage())
                              : null,
                        ),
                        const Text('Users',
                            style: TextStyle(color: AppColors.whiteColor)),
                      ],
                    ),

                  if (roles != null &&
                      roles!.contains(
                          'Admin')) // Check if the user has an 'Admin' role
                    Row(
                      children: [
                        IconButton(
                          icon: Icon(Icons.notification_add,
                              color: AppColors.whiteColor),
                          onPressed: userId != null
                              ? () {
                                  Get.to(() => NotificationForm());
                                }
                              : null,
                        ),
                        const Text('Notifications',
                            style: TextStyle(color: AppColors.whiteColor)),
                      ],
                    ),
                  const SizedBox(height: 10.0),
                  // About Us
                  Row(
                    children: [
                      IconButton(
                        icon: Icon(Icons.info, color: AppColors.whiteColor),
                        onPressed: userId != null
                            ? () => Get.to(() => AboutUsPage())
                            : null,
                      ),
                      const Text('About Us',
                          style: TextStyle(color: AppColors.whiteColor)),
                    ],
                  ),

                  const SizedBox(height: 500.0),
                  const Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Text('With 🤍 by OND',
                          style: TextStyle(color: AppColors.whiteColor)),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

// Clipper for drawer UI effect
class OvalRightBorderClipper extends CustomClipper<Path> {
  @override
  Path getClip(Size size) {
    var path = Path();
    path.lineTo(0, 0);
    path.lineTo(size.width - 40, 0);
    path.quadraticBezierTo(
        size.width, size.height / 4, size.width, size.height / 2);
    path.quadraticBezierTo(size.width, size.height - (size.height / 4),
        size.width - 40, size.height);
    path.lineTo(0, size.height);
    return path;
  }

  @override
  bool shouldReclip(CustomClipper<Path> oldClipper) {
    return true;
  }
}
