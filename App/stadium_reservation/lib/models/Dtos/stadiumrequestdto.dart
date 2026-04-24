class StadiumRequestDto {
  String name;
  int priceInHour;
  String type;
  String noOfPlayers;
  String stadiumCenterId;

  StadiumRequestDto({
    required this.name,
    required this.priceInHour,
    required this.type,
    required this.noOfPlayers,
    required this.stadiumCenterId,
  });

  // Convert the DTO to a JSON object to send in the POST request
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'priceinHour': priceInHour,
      'type': type,
      'noOfPlayers': noOfPlayers,
      'stadiumCenterId': stadiumCenterId,
    };
  }
}
