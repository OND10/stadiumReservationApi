class ApiEndPoints {
  static final String baseUrl = "https://localhost:7049/api";
  static _AuthEndPoints authEndPoints = _AuthEndPoints();
  static _StadiumImageEndPoints imageEndPoints = _StadiumImageEndPoints();
  static _StadiumCenterEndPoints stadiumCenterEndPoints =
      _StadiumCenterEndPoints();
  static _StadiumEndPoints stadiumEndPoints = _StadiumEndPoints();
  static _WorkDaysEndPoints workDaysEndPoints = _WorkDaysEndPoints();
  static _CenterBookingEndPoints centerBookingEndPoints =
      _CenterBookingEndPoints();
}

class _AuthEndPoints {
  final String registerUser = "/Auth/register";
  final String loginUser = "/Auth/login";
  final String getUserById = "/Auth/getUser/{userId}";
  final String updateUser = "/Auth/updateUser/{userId}";
  final String getAllUsers = "/Auth";
  final String deleteUser = "/Auth/deleteUser/{userId}";
  final String getMessages = "/Auth/getUserMessage/{userId}";
  final String sendMessage = "/Auth/sendMessage";
  final String getMessagesCount = "/Auth/getUserMessagesCount/{userId}";
}

class _StadiumImageEndPoints {
  final String getStadium = "/StadiumImage";
  final String getStadiumById = "/StadiumImage/getImage/{stadiumId}";
}

class _StadiumCenterEndPoints {
  final String getAllStadiumCenter = "/StadiumCenter/getCenter";
  final String getStadiumCenterById = "/StadiumCenter/{id}";
}

class _StadiumEndPoints {
  final String getStadiumByCenter = "/Stadium/getByCenter/{centerId}";
  final String getStadiumById = "/Stadium/getStadium/{stadiumId}";
}

class _WorkDaysEndPoints {
  final String getwordDaysCenter = "/WorkDay/getworkDay/{centerId}";
}

class _CenterBookingEndPoints {
  final String CenterBooking = "/CenterBooking";
}
