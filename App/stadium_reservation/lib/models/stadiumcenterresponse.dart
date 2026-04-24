class Stadiumcenterresponse {
  String? id;
  String? name;
  String? phoneNumber;
  String? location;
  bool? dressingRoomVisible;
  String? owned_By;
  String? imageUrl;

  Stadiumcenterresponse({
    this.id,
    this.name,
    this.phoneNumber,
    this.location,
    this.dressingRoomVisible,
    this.owned_By,
    this.imageUrl,
  });

  factory Stadiumcenterresponse.fromJson(Map<String, dynamic> json) {
    return Stadiumcenterresponse(
      id: json['id'],
      name: json['name'],
      phoneNumber: json['phoneNumber'],
      location: json['location'],
      dressingRoomVisible: json['dressingRoomVisible'],
      owned_By: json['owned_By'],
      imageUrl: json['imageUrl'],
    );
  }
}
