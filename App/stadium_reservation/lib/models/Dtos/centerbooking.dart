class CenterBooking {
  String userId;
  String centerId;
  DateTime beginTime;
  DateTime endTime;
  String paymentMethod;
  String? exchangeNumber;

  CenterBooking({
    required this.userId,
    required this.centerId,
    required this.beginTime,
    required this.endTime,
    required this.paymentMethod,
    this.exchangeNumber,
  });

  Map<String, dynamic> toJson() {
    return {
      'userId': userId,
      'centerId': centerId,
      'beginTime': beginTime.toIso8601String(),
      'endTime': endTime.toIso8601String(),
      'paymentMethod': paymentMethod,
      'exchangeNumber': exchangeNumber,
    };
  }
}
