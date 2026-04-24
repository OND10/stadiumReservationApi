class Workdayrequestdto {
  String stadiumCenterId;
  int dayOfWeek;
  String beginWorkTime;
  String endWorkTime;

  Workdayrequestdto({
    required this.stadiumCenterId,
    required this.dayOfWeek,
    required this.beginWorkTime,
    required this.endWorkTime,
  });

  Map<String, dynamic> toJson() {
    return {
      'stadiumCenterId': stadiumCenterId,
      'dayOfWeek': dayOfWeek,
      'beginWorkTime': beginWorkTime,
      'endWorkTime': endWorkTime,
    };
  }
}
