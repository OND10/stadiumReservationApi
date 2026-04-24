class Userresponsedto {
  String? id;
  String? name;
  String? email;
  String? phoneNumber;
  String? noneHashedPassword;
  String? imageUrl;

  Userresponsedto(
      {this.id,
      this.name,
      this.email,
      this.phoneNumber,
      this.noneHashedPassword,
      this.imageUrl});

  factory Userresponsedto.fromJson(Map<String, dynamic> json) {
    return Userresponsedto(
        id: json['id'],
        name: json['name'],
        email: json['email'],
        phoneNumber: json['phoneNumber'],
        noneHashedPassword: json['noneHashedPassword'],
        imageUrl: json['imageUrl']);
  }
}
