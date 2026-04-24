import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/user_controller.dart';
import 'package:stadium_reservation/views/home_page.dart';

class NotificationForm extends StatelessWidget {
  final UserController controller = Get.put(UserController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Send Notification')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            Container(
              decoration: BoxDecoration(
                image: DecorationImage(
                  image: AssetImage('assets/images/ground.png'),
                  fit: BoxFit.cover,
                ),
              ),
            ),
            Center(
              child: TextField(
                controller: controller.messageController,
                decoration: const InputDecoration(
                  labelText: 'Message',
                  border: OutlineInputBorder(),
                ),
              ),
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                controller.sendNotification();
              },
              child: const Text('Send Notification'),
            ),
          ],
        ),
      ),
    );
  }
}
