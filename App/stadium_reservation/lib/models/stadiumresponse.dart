class Stadiumresponse {
  final String? id;
  final String? name;
  final double? priceInHour;
  final String? type;
  final String? noOfPlayers;
  final String? stadiumCenterId;

  Stadiumresponse({
     this.id,
     this.name,
     this.priceInHour,
     this.type,
     this.noOfPlayers,
     this.stadiumCenterId,
  });

  factory Stadiumresponse.fromJson(Map<String, dynamic> json) {
    return Stadiumresponse(
      id: json['id'],
      name: json['name'],
      priceInHour: json['priceinHour'],
      type: json['type'],
      noOfPlayers: json['noOfplayers'],
      stadiumCenterId: json['stadiumCenterId'],
    );
  }
}
