// // lib/pages/stadiumspage.dart
// import 'package:flutter/material.dart';
// import 'package:get/get.dart';
// import 'package:stadium_reservation/controllers/login_controller.dart';
// import 'package:stadium_reservation/controllers/stadium_controller.dart';

// class StadiumsPage extends StatelessWidget {
//   final String centerId; // Center ID passed from ProductPage
//   final StadiumController stadiumController = Get.put(StadiumController());
//   final LoginController loginController = Get.put(LoginController());

//   StadiumsPage({Key? key, required this.centerId}) : super(key: key);

//   @override
//   Widget build(BuildContext context) {
//     // Fetch stadiums when the page is initialized
//     stadiumController.getStadiumByCenterId(centerId);

//     return Scaffold(
//       appBar: AppBar(
//         title: Text('Stadiums'),
//       ),
//       body: Container(
//         decoration: BoxDecoration(
//             image: DecorationImage(
//                 image: AssetImage("assets/images/ground.png"),
//                 fit: BoxFit.cover)),
//         child: Center(
//           child: Obx(() {
//             if (stadiumController.isLoading.value) {
//               return Center(child: CircularProgressIndicator());
//             } else if (stadiumController.stadiums.isEmpty) {
//               return Center(child: Text('No stadiums found for this center.'));
//             } else {
//               return ListView.builder(
//                 itemCount: stadiumController.stadiums.length,
//                 itemBuilder: (context, index) {
//                   final stadium = stadiumController.stadiums[index];
//                   return ListTile(
//                     leading: Text(
//                       stadium.type,
//                       style: TextStyle(color: Colors.white),
//                     ),
//                     title: Text(
//                       stadium.name,
//                       style: TextStyle(color: Colors.white),
//                     ),
//                     subtitle: Text(
//                       stadium.noOfPlayers,
//                       style: TextStyle(color: Colors.white),
//                     ),
//                     onTap: () {
//                       // Handle tap on stadium
//                       // For example, navigate to a detailed stadium page
//                     },
//                   );
//                 },
//               );
//             }
//           }),
//         ),
//       ),
//       drawer: _buildDrawer(context, loginController),
//     );
//   }
// }

// Widget _buildDrawer(BuildContext context, LoginController loginController) {
//   return Drawer(
//     child: ListView(
//       padding: EdgeInsets.zero,
//       children: <Widget>[
//         UserAccountsDrawerHeader(
//           accountName: Text("${loginController.userNamecontroller}"),
//           accountEmail: Text(""),
//           currentAccountPicture: CircleAvatar(
//             backgroundImage: NetworkImage(
//               'https://example.com/user-profile.png',
//             ),
//           ),
//           decoration: BoxDecoration(
//             color: Colors.black87,
//           ),
//         ),
//         _buildDrawerItem(
//           icon: Icons.home,
//           text: 'Home',
//           onTap: () {},
//         ),
//         _buildDrawerItem(
//           icon: Icons.admin_panel_settings,
//           text: 'Admin',
//           onTap: () {},
//         ),
//         _buildDrawerItem(
//           icon: Icons.explore,
//           text: 'Explorer',
//           onTap: () {},
//         ),
//         _buildDrawerItem(
//           icon: Icons.settings,
//           text: 'Settings',
//           onTap: () {},
//         ),
//         _buildDrawerItem(
//           icon: Icons.help,
//           text: 'Help',
//           onTap: () {},
//         ),
//         Divider(),
//         _buildDrawerItem(
//           icon: Icons.logout,
//           text: 'Log out',
//           onTap: () => loginController.logout(),
//         ),
//       ],
//     ),
//   );
// }

// Widget _buildDrawerItem(
//     {required IconData icon, required String text, GestureTapCallback? onTap}) {
//   return ListTile(
//     title: Row(
//       children: <Widget>[
//         Icon(icon),
//         Padding(
//           padding: EdgeInsets.only(left: 8.0),
//           child: Text(text),
//         )
//       ],
//     ),
//     onTap: onTap,
//   );
// }
