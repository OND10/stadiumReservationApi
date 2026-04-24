import 'package:http/http.dart' as http;
import 'dart:convert';

import 'package:stadium_reservation/shared/api_endpoints.dart';

class Userservice {
  // Get user messages
  static Future<ApiResponse<List<String>>> getUserMessages(
      String userId) async {
    final response = await http.get(
      Uri.parse(ApiEndPoints.baseUrl +
          ApiEndPoints.authEndPoints.getMessages
              .replaceFirst("{userId}", userId)),
    );
    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      List<String> messages =
          List<String>.from(data['data']); // Parse list of strings
      return ApiResponse<List<String>>.success(messages);
    } else {
      return ApiResponse<List<String>>.error('Failed to fetch messages');
    }
  }

  static Future<ApiResponse<int>> getMessagesCount(String userId) async {
    final response = await http.get(
      Uri.parse(ApiEndPoints.baseUrl +
          ApiEndPoints.authEndPoints.getMessagesCount
              .replaceFirst("{userId}", userId)),
    );
    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      int count = data['data']; // Parse list of strings
      return ApiResponse<int>.success(count);
    } else {
      return ApiResponse<int>.error('Failed to fetch messages count');
    }
  }

  // Send notification to all users
  static Future<bool> sendNotificationToAll(String message) async {
    final response = await http.post(
      Uri.parse(ApiEndPoints.baseUrl + ApiEndPoints.authEndPoints.sendMessage),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(message),
    );
    return response.statusCode == 200;
  }
}

// Helper class for API responses
class ApiResponse<T> {
  final T? data;
  final bool isSuccess;
  final String? errorMessage;

  ApiResponse.success(this.data)
      : isSuccess = true,
        errorMessage = null;
  ApiResponse.error(this.errorMessage)
      : isSuccess = false,
        data = null;
}
