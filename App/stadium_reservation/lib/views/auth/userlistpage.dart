import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';
import 'package:stadium_reservation/models/Dtos/Auth/userresponsedto.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

class UserListPage extends StatelessWidget {
  final UserController userController = Get.put(UserController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Users'),
      ),
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage("assets/images/ground.png"),
            fit: BoxFit.cover,
          ),
        ),
        child: Obx(() {
          if (userController.isLoading.value) {
            return Center(child: CircularProgressIndicator());
          }
          if (userController.users.isEmpty) {
            return Center(child: Text('No users found'));
          }
          return UserListWithAnimation();
        }),
      ),
    );
  }
}

class UserListWithAnimation extends StatelessWidget {
  final UserController userController = Get.find<UserController>();

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: userController.users.length,
      itemBuilder: (context, index) {
        final user = userController.users[index];
        return AnimatedUserListItem(user: user, index: index);
      },
    );
  }
}

class AnimatedUserListItem extends StatefulWidget {
  final Userresponsedto user;
  final int index;

  const AnimatedUserListItem(
      {Key? key, required this.user, required this.index})
      : super(key: key);

  @override
  _AnimatedUserListItemState createState() => _AnimatedUserListItemState();
}

class _AnimatedUserListItemState extends State<AnimatedUserListItem>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;
  late Animation<double> _opacityAnimation;

  @override
  void initState() {
    super.initState();

    // Animation controller for handling the slide and fade out effect
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 500),
    );

    // Slide out animation to the left
    _slideAnimation = Tween<Offset>(
      begin: Offset(0, 0), // Initial position (no offset)
      end: Offset(-1, 0), // Slide out to the left
    ).animate(CurvedAnimation(parent: _controller, curve: Curves.easeOut));

    // Fade out animation
    _opacityAnimation = Tween<double>(
      begin: 1, // Fully visible
      end: 0, // Fully invisible
    ).animate(CurvedAnimation(parent: _controller, curve: Curves.easeOut));
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _deleteUser() {
    _controller.forward().then((_) {
      Get.find<UserController>().deleteUserById(widget.user.id!);
    });
  }

  @override
  Widget build(BuildContext context) {
    return SlideTransition(
      position: _slideAnimation,
      child: FadeTransition(
        opacity: _opacityAnimation,
        child: GestureDetector(
          onTap: () {
            // Show a confirmation dialog before starting the deletion
            Get.defaultDialog(
              title: 'Delete Confirmation',
              middleText:
                  'Are you sure you want to delete ${widget.user.name}?',
              textConfirm: 'Delete',
              textCancel: 'Cancel',
              onConfirm: () {
                Get.back(); // Close the dialog
                _deleteUser(); // Start the animation and delete the user
              },
            );
          },
          child: UserListItem(user: widget.user),
        ),
      ),
    );
  }
}

class UserListItem extends StatelessWidget {
  final Userresponsedto user;

  const UserListItem({Key? key, required this.user}) : super(key: key);

  Future<String?> _getUserName() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString('userName');
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<String?>(
      future: _getUserName(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          // Show a loading spinner while waiting for SharedPreferences
          return CircularProgressIndicator();
        } else if (snapshot.hasError) {
          return Text('Error loading user data');
        } else {
          String? userName = snapshot.data;

          return Padding(
            padding: const EdgeInsets.only(top: 15),
            child: Card(
              color: Color.fromARGB(255, 66, 66, 66).withOpacity(0.2),
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(15)),
              child: ListTile(
                leading: CircleAvatar(
                  backgroundImage: user.imageUrl != null
                      ? NetworkImage(user.imageUrl!)
                      : AssetImage('assets/images/user_placeholder.png')
                          as ImageProvider,
                ),
                title: Text(
                  // Check if the user from SharedPreferences is the same as the current user
                  user.name == userName
                      ? '${user.name} (Me)'
                      : user.name ?? 'Unknown',
                  style: TextStyle(color: AppColors.whiteColor),
                ),
                subtitle: Text(
                  user.email ?? 'No email',
                  style: TextStyle(color: AppColors.whiteColor),
                ),
              ),
            ),
          );
        }
      },
    );
  }
}
