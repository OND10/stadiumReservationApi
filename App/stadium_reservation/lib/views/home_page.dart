// import 'dart:ffi';
// import 'dart:nativewrappers/_internal/vm/lib/ffi_native_type_patch.dart';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:stadium_reservation/controllers/login_controller.dart';
import 'package:stadium_reservation/controllers/pitchimage_controller.dart';
import 'package:stadium_reservation/controllers/stadiumcenter_controller.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';
import 'package:stadium_reservation/main.dart';
import 'package:stadium_reservation/views/NotificationPage.dart';
import 'package:stadium_reservation/views/auth/userprofile.dart';
import 'package:stadium_reservation/views/components/app_icons.dart';
import 'package:stadium_reservation/views/components/cards.dart';
import 'package:stadium_reservation/views/components/drawer.dart';
import 'package:stadium_reservation/views/components/textApp.dart';
import 'package:stadium_reservation/views/StadiumImagePage.dart';

class HomePage extends StatefulWidget {
  @override
  _HomePage createState() => _HomePage();
}

class _HomePage extends State<HomePage> {
  final PitchImageController pitchController = Get.put(PitchImageController());
  final LoginController loginController = Get.put(LoginController());
  final UserController userController = Get.put(UserController());
  final StadiumcenterController stadiumCenterController =
      Get.put(StadiumcenterController());
  late int _currentPage;
  String? userId;

  @override
  void initState() {
    _currentPage = 0;
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Stadiums'),
        actions: [
          IconButton(
            icon: Icon(Icons.notifications, color: Colors.black),
            onPressed: () async {
              // Retrieve userId from SharedPreferences
              SharedPreferences prefs = await SharedPreferences.getInstance();
              userId = prefs.getString('userId');

              if (userId != null) {
                // Fetch messages for the user
                await userController.getUserMessages(userId!);

                // Navigate to NotificationPage
                Get.to(() => NotificationPage());
              } else {
                Get.snackbar('Error', 'User ID not found!');
              }
            },
          ),
        ],
      ),
      body: getPage(_currentPage),
      drawer: AppDrawer(),
      bottomNavigationBar: AnimatedBottomNav(
          currentIndex: _currentPage,
          onChange: (index) {
            setState(() {
              _currentPage = index;
            });
          }),
    );
  }

  getPage(int page) {
    switch (page) {
      case 0:
        return Container(
          decoration: BoxDecoration(
              image: DecorationImage(
                  image: AssetImage("assets/images/ground.png"),
                  fit: BoxFit.cover)),
          child: Obx(() {
            if (pitchController.isLoading.value) {
              return Center(child: CircularProgressIndicator());
            } else {
              return Column(
                children: [
                  SizedBox(
                    height: 100,
                  ),
                  Row(
                    children: [
                      Container(
                        margin: EdgeInsets.only(left: 70),
                        child: Text(
                          "What are you in \n  the mood for ?",
                          style: TextStyle(
                            color: Colors.white,
                            fontSize: 24,
                          ),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(
                    height: 20,
                  ),
                  CarouselSlider(
                    options: CarouselOptions(
                      height: 200.0,
                      autoPlay: true,
                    ),
                    items: pitchController.pitchImages.map((image) {
                      return Builder(
                        builder: (BuildContext context) {
                          return ClipRRect(
                            borderRadius: BorderRadius.circular(
                                20), // Set the border radius to 20
                            child: Image.network(
                              image.imageUrl,
                              fit: BoxFit
                                  .cover, // Ensure the image covers the entire area
                            ),
                          );
                        },
                      );
                    }).toList(),
                  ),
                  Container(
                    margin: const EdgeInsets.symmetric(vertical: 10),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      mainAxisSize: MainAxisSize.max,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Row(
                          verticalDirection: VerticalDirection.up,
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          mainAxisSize: MainAxisSize.max,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Container(
                                margin: const EdgeInsets.only(left: 65),
                                child: TextTitle2(text: 'Top rated')),
                            Container(
                                margin:
                                    const EdgeInsets.symmetric(horizontal: 10),
                                child: GestureDetector(
                                    onTap: () {
                                      Get.to(() => const MyApp());
                                    },
                                    child: Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.center,
                                      children: [
                                        Textsub(text: 'View All'),
                                        ICon(
                                            iconic: Icons.chevron_right,
                                            colorIcon: const Color.fromARGB(
                                                255, 255, 255, 255))
                                      ],
                                    )))
                          ],
                        ),
                        const SizedBox(
                          height: 10,
                        ),
                        SizedBox(
                          height: 200,
                          child: SingleChildScrollView(
                            scrollDirection: Axis.horizontal,
                            child: Container(
                              margin: EdgeInsets.only(left: 34),
                              child: Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: stadiumCenterController.stadiumcenters
                                    .map((stadium) {
                                  return GestureDetector(
                                    onTap: () {
                                      if (stadium.id != null) {
                                        Get.to(() => StadiumImagePage(
                                            centerId: stadium.id!));
                                      } else {
                                        // Handle the case where the ID is null
                                        Get.snackbar(
                                            "Error", "Invalid stadium ID");
                                      }
                                    },
                                    child: StaduimCard(
                                      id: stadium.id!,
                                      name: stadium.name!,
                                      phoneNumber: stadium.phoneNumber!,
                                      location: stadium.location!,
                                      dressingRoomVisible:
                                          stadium.dressingRoomVisible!,
                                      imageUrl: stadium.imageUrl!,
                                      owned_By: stadium.owned_By!,
                                      context: context,
                                    ),
                                  );
                                }).toList(),
                              ),
                            ),
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              );
            }
          }),
        );
      case 1:
        return Center(
            child: Container(
                child: Text("Profile Page",
                    style: TextStyle(fontSize: 16, color: Colors.black))));
      case 2:
        Get.to(UserProfilePage(userId: userId!));
    }
  }
}

class AnimatedBottomNav extends StatelessWidget {
  final int currentIndex;
  final Function(int) onChange;
  const AnimatedBottomNav(
      {Key? key, required this.currentIndex, required this.onChange})
      : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Container(
      height: kToolbarHeight,
      decoration: BoxDecoration(color: Color.fromARGB(255, 24, 39, 29)),
      child: Row(
        children: <Widget>[
          Expanded(
            child: InkWell(
              onTap: () => onChange(0),
              child: BottomNavItem(
                icon: Icons.home,
                title: "Home",
                isActive: currentIndex == 0,
              ),
            ),
          ),
          Expanded(
            child: InkWell(
              onTap: () => onChange(1),
              child: BottomNavItem(
                icon: Icons.favorite,
                title: "Favorites",
                isActive: currentIndex == 1,
              ),
            ),
          ),
          Expanded(
            child: InkWell(
              onTap: () => onChange(2),
              child: BottomNavItem(
                icon: Icons.verified_user,
                title: "Profile",
                isActive: currentIndex == 2,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class BottomNavItem extends StatelessWidget {
  final bool isActive;
  final IconData icon;
  final Color? activeColor;
  final Color? inactiveColor;
  final String title;
  const BottomNavItem(
      {Key? key,
      this.isActive = false,
      required this.icon,
      this.activeColor,
      this.inactiveColor,
      required this.title})
      : super(key: key);
  @override
  Widget build(BuildContext context) {
    return AnimatedSwitcher(
      transitionBuilder: (child, animation) {
        return SlideTransition(
          position: Tween<Offset>(
            begin: const Offset(0.0, 1.0),
            end: Offset.zero,
          ).animate(animation),
          child: child,
        );
      },
      duration: Duration(milliseconds: 500),
      reverseDuration: Duration(milliseconds: 200),
      child: isActive
          ? Container(
              color: Color.fromARGB(255, 255, 255, 255),
              padding: const EdgeInsets.all(8.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Text(
                    title,
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      color: activeColor ?? Theme.of(context).primaryColor,
                    ),
                  ),
                  const SizedBox(height: 5.0),
                  Container(
                    width: 5.0,
                    height: 5.0,
                    decoration: BoxDecoration(
                      shape: BoxShape.circle,
                      color: activeColor ?? Theme.of(context).primaryColor,
                    ),
                  ),
                ],
              ),
            )
          : Icon(
              icon,
              color: inactiveColor ?? Colors.white,
            ),
    );
  }
}
