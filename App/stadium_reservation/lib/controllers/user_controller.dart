import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:get/get_connect/http/src/request/request.dart';
import 'package:http/http.dart' as http;
import 'package:get/get.dart';
import 'package:stadium_reservation/models/Dtos/Auth/updateuserrequestdto.dart';
import 'package:stadium_reservation/models/Dtos/Auth/userresponsedto.dart';
import 'package:stadium_reservation/services/userservice.dart';
import 'package:stadium_reservation/shared/api_endpoints.dart';
import 'package:stadium_reservation/views/home_page.dart';

class UserController extends GetxController {
  var isLoading = false.obs;
  var user = Userresponsedto().obs; // Holds user data
  var users = <Userresponsedto>[].obs;
  RxList<String> userMessages = <String>[].obs;
  final messageController = TextEditingController();
  var count = 0.obs;

  // Controllers for form inputs
  final nameController = TextEditingController();
  final phoneNumberController = TextEditingController();

  @override
  void onInit() {
    super.onInit();
    getAllUsers();
  }

  //getMessages user
  Future<void> getUserMessages(String userId) async {
    final result = await Userservice.getUserMessages(userId);
    if (result.isSuccess) {
      userMessages.value = result.data ?? [];
    } else {
      Get.snackbar('Error', 'Failed to load messages');
    }
  }

  //getCounts user
  Future<void> getUserMessagesCount(String userId) async {
    final result = await Userservice.getMessagesCount(userId);
    if (result.isSuccess) {
      count.value = result.data!;
    } else {
      Get.snackbar('Error', 'Failed to load messages count');
    }
  }

  //sendMessage admin
  Future<void> sendNotification() async {
    final message = messageController.text.trim();

    if (message.isEmpty) {
      Get.snackbar('Error', 'Message cannot be empty');
      return;
    }

    bool success = await Userservice.sendNotificationToAll(message);

    if (success) {
      Get.snackbar('Success', 'Notification sent successfully');
      Get.to(HomePage()); // Navigate to Home Page
    } else {
      Get.snackbar('Error', 'Failed to send notification');
    }
  }

  //getAll users
  Future<void> getAllUsers() async {
    var url = Uri.parse(
        ApiEndPoints.baseUrl + ApiEndPoints.authEndPoints.getAllUsers);
    var response = await http.get(url);

    if (response.statusCode == 200) {
      var jsonResponse = json.decode(response.body);
      if (jsonResponse['isSuccess']) {
        var data = jsonResponse['data'] as List;
        users.value =
            data.map((item) => Userresponsedto.fromJson(item)).toList();
      }
    }
  }

  // Fetch user data by ID
  Future<void> getUserById(String userId) async {
    isLoading.value = true;
    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.authEndPoints.getUserById
            .replaceFirst("{userId}", userId));

    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        var data = json.decode(response.body)['data'];
        user.value = Userresponsedto.fromJson(data);

        // Set initial values in the form
        nameController.text = user.value.name ?? '';
        phoneNumberController.text = user.value.phoneNumber ?? '';
      } else {
        Get.snackbar('Error', 'Failed to load user details');
      }
    } catch (e) {
      Get.snackbar('Error', 'An error occurred while fetching user data');
    } finally {
      isLoading.value = false;
    }
  }

  // Update user information by ID
  Future<void> updateUser(String userId) async {
    isLoading.value = true;

    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.authEndPoints.updateUser.replaceFirst("{userId}", userId));

    // Create the request DTO with form data
    var updateUserRequestDto = Updateuserrequestdto(
      name: nameController.text,
      phoneNumber: phoneNumberController.text,
    );

    try {
      final response = await http.put(
        url,
        headers: {"Content-Type": "application/json"},
        body: jsonEncode(updateUserRequestDto.toJson()),
      );

      if (response.statusCode == 200) {
        Get.snackbar('Success', 'User updated successfully');
        // Optionally, fetch the updated user data again
        await getUserById(userId);

        Get.to(() => HomePage());
      } else {
        Get.snackbar('Error', 'Failed to update user');
      }
    } catch (e) {
      Get.snackbar('Error', 'An error occurred while updating user data');
    } finally {
      isLoading.value = false;
    }
  }

  //delete user by id
  Future<void> deleteUserById(String userId) async {
    var url = Uri.parse(
        '${ApiEndPoints.baseUrl}${ApiEndPoints.authEndPoints.deleteUser.replaceFirst("{userId}", userId)}');
    var response = await http.delete(url);

    if (response.statusCode == 200) {
      // Remove user from the list upon successful deletion
      users.removeWhere((user) => user.id == userId);
      Get.snackbar('Success', 'User is deleted Successfully',
          snackPosition: SnackPosition.BOTTOM);
    } else {
      Get.snackbar('Error', 'Failed to delete user',
          snackPosition: SnackPosition.BOTTOM);
    }
  }
}
