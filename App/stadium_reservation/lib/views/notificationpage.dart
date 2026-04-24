import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';

class NotificationPage extends StatelessWidget {
  final UserController userController = Get.find<UserController>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Your Notifications')),
      body: Container(
        decoration: const BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/images/ground.png'),
            fit: BoxFit.cover,
          ),
        ),
        child: Center(
          child: Obx(() {
            final messages = userController.userMessages;
            if (messages.isEmpty) {
              return const Text(
                'No unread messages',
                style: TextStyle(fontSize: 18),
              );
            }
            return ListView.builder(
              physics: BouncingScrollPhysics(),
              padding: const EdgeInsets.all(16),
              itemCount: messages.length,
              itemBuilder: (context, index) {
                return Card(
                  margin: const EdgeInsets.symmetric(vertical: 8),
                  child: ListTile(
                    leading: const Icon(Icons.notifications),
                    title: Text(
                      messages[index],
                      style: const TextStyle(fontSize: 16),
                    ),
                  ),
                );
              },
            );
          }),
        ),
      ),
    );
  }
}
