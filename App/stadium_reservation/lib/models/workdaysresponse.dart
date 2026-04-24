class Workdaysresponse {
  String? StadiumCenterId;
  String? dayOfWeek;
  String? beginWorkTime;
  String? endWorkTime;

  Workdaysresponse(
      {this.StadiumCenterId,
      this.beginWorkTime,
      this.dayOfWeek,
      this.endWorkTime});

  factory Workdaysresponse.fromJson(Map<String, dynamic> json) {
    return Workdaysresponse(
        StadiumCenterId: json['stadiumCenterId'],
        dayOfWeek: json['dayOfWeek'],
        beginWorkTime: json['beginWorkTime'],
        endWorkTime: json['endWorkTime']);
  }
}
