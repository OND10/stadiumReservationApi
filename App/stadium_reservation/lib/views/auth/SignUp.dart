// import 'dart:io';
// import 'dart:typed_data';
// import 'package:flutter/foundation.dart'; // For kIsWeb
// import 'package:flutter/material.dart';
// import 'package:get/get.dart';
// import 'package:image_picker/image_picker.dart';
// import 'package:stadium_reservation/controllers/register_controller.dart';
// import 'package:stadium_reservation/views/auth/login.dart';

// class SignUp extends StatefulWidget {
//   const SignUp({super.key});

//   @override
//   State<SignUp> createState() => _SignUpState();
// }

// class _SignUpState extends State<SignUp> {
//   final TextEditingController _userNameController = TextEditingController();
//   final TextEditingController _emailController = TextEditingController();
//   final TextEditingController _passwordController = TextEditingController();
//   final TextEditingController _phoneNumberController = TextEditingController();

//   File? _imageFile;
//   Uint8List? _webImage; // This will be used for Flutter Web

//   final ImagePicker _picker = ImagePicker();

//   RegisterController registerController = Get.put(RegisterController());
//   bool _obscureText = true;

//   void _togglePasswordVisibility() {
//     setState(() {
//       _obscureText = !_obscureText;
//     });
//   }

//   Future<void> _pickImage() async {
//     final pickedFile = await _picker.pickImage(source: ImageSource.gallery);

//     if (pickedFile != null) {
//       if (kIsWeb) {
//         // If it's on the web, store the image as bytes
//         final bytes = await pickedFile.readAsBytes();
//         setState(() {
//           _webImage = bytes;
//         });
//       } else {
//         // If it's on mobile or desktop, store the file
//         setState(() {
//           _imageFile = File(pickedFile.path);
//         });
//       }
//     }
//   }

//   void _choose() async {
//     final pickedFile = await _picker.pickImage(
//         source: ImageSource.gallery,
//         imageQuality: 50,
//         maxHeight: 500,
//         maxWidth: 500);
//     setState(() {
//       if (pickedFile != null) {
//         _imageFile = File(pickedFile.path);
//       } else {
//         print('No image selected.');
//       }
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     return Scaffold(
//       body: Stack(
//         fit: StackFit.expand,
//         children: [
//           Image.asset(
//             'assets/images/ground.png',
//             fit: BoxFit.cover,
//           ),
//           Center(
//             child: Padding(
//               padding: const EdgeInsets.symmetric(horizontal: 24.0),
//               child: SingleChildScrollView(
//                 child: Column(
//                   mainAxisAlignment: MainAxisAlignment.center,
//                   children: [
//                     Text(
//                       'Sign Up',
//                       style: TextStyle(
//                         color: Colors.white,
//                         fontSize: 36,
//                         fontWeight: FontWeight.bold,
//                       ),
//                     ),
//                     SizedBox(height: 16),

//                     // Email Input
//                     TextFormField(
//                       decoration: InputDecoration(
//                         prefixIcon: Icon(Icons.email, color: Colors.white),
//                         filled: true,
//                         fillColor: Colors.white24,
//                         hintText: 'Email',
//                         hintStyle: TextStyle(color: Colors.white),
//                         border: OutlineInputBorder(
//                           borderRadius: BorderRadius.circular(8),
//                           borderSide: BorderSide.none,
//                         ),
//                       ),
//                       style: const TextStyle(color: Colors.white),
//                       controller: registerController.emailcontroller,
//                     ),
//                     SizedBox(height: 16),

//                     // Username Input
//                     TextFormField(
//                       decoration: InputDecoration(
//                         prefixIcon: Icon(Icons.person, color: Colors.white),
//                         filled: true,
//                         fillColor: Colors.white24,
//                         hintText: 'Username',
//                         hintStyle: TextStyle(color: Colors.white),
//                         border: OutlineInputBorder(
//                           borderRadius: BorderRadius.circular(8),
//                           borderSide: BorderSide.none,
//                         ),
//                       ),
//                       style: const TextStyle(color: Colors.white),
//                       controller: registerController.userNamecontroller,
//                     ),
//                     SizedBox(height: 16),

//                     // Password Input
//                     TextFormField(
//                       obscureText: _obscureText,
//                       decoration: InputDecoration(
//                         prefixIcon: Icon(Icons.lock, color: Colors.white),
//                         hintText: 'Password',
//                         hintStyle: TextStyle(color: Colors.white),
//                         filled: true,
//                         fillColor: Colors.white24,
//                         border: OutlineInputBorder(
//                           borderRadius: BorderRadius.circular(8),
//                           borderSide: BorderSide.none,
//                         ),
//                         suffixIcon: IconButton(
//                           icon: Icon(
//                             _obscureText
//                                 ? Icons.visibility
//                                 : Icons.visibility_off,
//                             color: Colors.white,
//                           ),
//                           onPressed: _togglePasswordVisibility,
//                         ),
//                       ),
//                       style: TextStyle(color: Colors.white),
//                       controller: registerController.passwordcontroller,
//                     ),
//                     SizedBox(height: 16),

//                     // Phone Number Input
//                     TextFormField(
//                       decoration: InputDecoration(
//                         prefixIcon: Icon(Icons.phone, color: Colors.white),
//                         filled: true,
//                         fillColor: Colors.white24,
//                         hintText: 'Phone Number',
//                         hintStyle: TextStyle(color: Colors.white),
//                         border: OutlineInputBorder(
//                           borderRadius: BorderRadius.circular(8),
//                           borderSide: BorderSide.none,
//                         ),
//                       ),
//                       style: const TextStyle(color: Colors.white),
//                       controller: registerController.phoneNumbercontroller,
//                     ),
//                     SizedBox(height: 16),

//                     // Image Picker
//                     GestureDetector(
//                       onTap: _pickImage,
//                       child: Container(
//                         width: double.infinity,
//                         height: 150,
//                         decoration: BoxDecoration(
//                           color: Colors.white24,
//                           borderRadius: BorderRadius.circular(8),
//                           border: Border.all(color: Colors.white),
//                         ),
//                         child: _imageFile != null
//                             ? Image.file(_imageFile!, fit: BoxFit.cover)
//                             : (_webImage != null
//                                 ? Image.memory(_webImage!, fit: BoxFit.cover)
//                                 : Center(
//                                     child: Column(
//                                       mainAxisAlignment:
//                                           MainAxisAlignment.center,
//                                       children: [
//                                         Icon(Icons.camera_alt,
//                                             color: Colors.white),
//                                         Text('Select Image',
//                                             style:
//                                                 TextStyle(color: Colors.white))
//                                       ],
//                                     ),
//                                   )),
//                       ),
//                     ),
//                     SizedBox(height: 32),

//                     // Submit Button
//                     ElevatedButton(
//                       onPressed: () {
//                         // Call the registration method
//                         print(
//                             "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
//                         registerController.regitserUser;
//                       },
//                       child: Text('Sign Up'),
//                       style: ElevatedButton.styleFrom(
//                         backgroundColor: Colors.transparent,
//                         foregroundColor: Colors.white,
//                         side: BorderSide(color: Colors.white, width: 2),
//                         padding:
//                             EdgeInsets.symmetric(horizontal: 100, vertical: 15),
//                         textStyle: TextStyle(fontSize: 18),
//                         shape: RoundedRectangleBorder(
//                           borderRadius: BorderRadius.circular(8),
//                         ),
//                         elevation: 0,
//                       ),
//                     ),
//                     SizedBox(height: 16),

//                     // Already have an account
//                     TextButton(
//                       onPressed: () {
//                         Navigator.of(context).pushReplacement(
//                             MaterialPageRoute(builder: (context) {
//                           return Login();
//                         }));
//                       },
//                       child: const Text(
//                         'Already have an account? Sign In',
//                         style: TextStyle(
//                           color: Colors.white,
//                           decoration: TextDecoration.underline,
//                         ),
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//             ),
//           ),
//         ],
//       ),
//     );
//   }
// }

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/register_controller.dart';
import 'package:stadium_reservation/views/auth/login.dart';

class SignUp extends StatefulWidget {
  const SignUp({super.key});

  @override
  State<SignUp> createState() => _SignUpState();
}

class _SignUpState extends State<SignUp> {
  final TextEditingController _userNameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  RegisterController registerController = Get.put(RegisterController());
  bool _obscureText = true;

  void _togglePasswordVisibility() {
    setState(() {
      _obscureText = !_obscureText;
    });
  }

  void _showErrorDialog(String message) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Error'),
          content: Text(message),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: Text('OK'),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        fit: StackFit.expand,
        children: [
          Image.asset(
            'assets/images/ground.png', // Make sure to place the image in the assets folder and declare it in pubspec.yaml
            fit: BoxFit.cover,
          ),
          Center(
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 24.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    'Sign Up',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 36,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(height: 16),
                  TextFormField(
                    keyboardType: TextInputType.text,
                    decoration: InputDecoration(
                      prefixIcon: Icon(Icons.email, color: Colors.white),
                      filled: true,
                      fillColor: Colors.white24,
                      hintText: 'Email',
                      hintStyle: TextStyle(color: Colors.white),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(8),
                        borderSide: BorderSide.none,
                      ),
                    ),
                    style: const TextStyle(color: Colors.white),
                    controller: registerController.emailcontroller,
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    validator: (value) {
                      if (value != null && !value.contains('@')) {
                        return "You must enter a valid email address";
                      }
                    },
                  ),
                  SizedBox(height: 16),
                  TextFormField(
                    keyboardType: TextInputType.emailAddress,
                    decoration: InputDecoration(
                      prefixIcon: Icon(Icons.person, color: Colors.white),
                      filled: true,
                      fillColor: Colors.white24,
                      hintText: 'UserName',
                      hintStyle: TextStyle(color: Colors.white),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(8),
                        borderSide: BorderSide.none,
                      ),
                    ),
                    style: const TextStyle(color: Colors.white),
                    controller: registerController.userNamecontroller,
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    validator: (value) {
                      if (value != null && value.length < 3) {
                        return "User name must be unique and contains at least 3 characters";
                      }
                    },
                  ),
                  SizedBox(height: 16),
                  TextFormField(
                    obscureText: _obscureText,
                    keyboardType: TextInputType.visiblePassword,
                    decoration: InputDecoration(
                      prefixIcon: Icon(Icons.lock, color: Colors.white),
                      hintText: 'Password',
                      hintStyle: TextStyle(color: Colors.white),
                      filled: true,
                      fillColor: Colors.white24,
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(8),
                        borderSide: BorderSide.none,
                      ),
                      suffixIcon: IconButton(
                        icon: Icon(
                          _obscureText
                              ? Icons.visibility
                              : Icons.visibility_off,
                          color: Colors.white,
                        ),
                        onPressed: _togglePasswordVisibility,
                      ),
                    ),
                    style: TextStyle(color: Colors.white),
                    controller: registerController.passwordcontroller,
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    validator: (value) {
                      if (value != null && value.length < 8) {
                        return "Your password must be more than 8 characters";
                      }
                    },
                  ),
                  SizedBox(height: 16),
                  TextFormField(
                    keyboardType: TextInputType.number,
                    decoration: InputDecoration(
                      prefixIcon: Icon(Icons.phone, color: Colors.white),
                      filled: true,
                      fillColor: Colors.white24,
                      hintText: 'PhoneNumber',
                      hintStyle: TextStyle(color: Colors.white),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(8),
                        borderSide: BorderSide.none,
                      ),
                    ),
                    style: const TextStyle(color: Colors.white),
                    controller: registerController.phoneNumbercontroller,
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    validator: (value) {
                      if (value != null && value.length < 3) {
                        return "User name must be unique and contains at least 3 characters";
                      }
                    },
                  ),
                  SizedBox(height: 32),
                  ElevatedButton(
                    onPressed: () => registerController.regitserUser(),
                    // String username = _userNameController.text;
                    // String email = _emailController.text;
                    // String password = _passwordController.text;

                    // if (username == "OND" &&
                    //     email == "osama@gmail.com" &&
                    //     password == "Osama2002#") {
                    //   // _showErrorDialog("Account is created Successfully");
                    //   Navigator.of(context).pushReplacement(
                    //       MaterialPageRoute(builder: (context) {
                    //     return Login();
                    //   }));
                    // }
                    // },
                    child: Text('Sign in'),
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.transparent,
                      foregroundColor: Colors.white,
                      side: BorderSide(color: Colors.white, width: 2),
                      padding:
                          EdgeInsets.symmetric(horizontal: 100, vertical: 15),
                      textStyle: TextStyle(fontSize: 18),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8),
                      ),
                      elevation: 0,
                    ),
                  ),
                  SizedBox(height: 16),
                  TextButton(
                    onPressed: () {
                      // Add functionality to navigate to sign up page
                      Navigator.of(context).pushReplacement(
                          MaterialPageRoute(builder: (context) {
                        return Login();
                      }));
                    },
                    child: const Text(
                      'Already have account? Signin ',
                      style: TextStyle(
                        color: Colors.white,
                        decoration: TextDecoration.underline,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
