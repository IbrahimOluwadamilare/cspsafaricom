<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('index');
});
Route::get('/terms-and-conditions', function () {
    return view('terms-and-conditions');
});
Route::get('/privacy-policy', function () {
    return view('privacy-policy');
});
Route::get('/send-mail/{$request}', 'v1\MailController@OperatorRegistration');

Route::get('/Login', 'Auth\AuthenticationController@Login')->name('login');

Route::get('/Dashboard','Auth\OperatorLoginController@Dashboard')->name('admin.dashboard');

Route::get('/Drivers', 'Auth\OperatorLoginController@Drivers');

Route::get('/Trips/{driverId}', 'Auth\OperatorLoginController@Trip');

Route::get('/Admin/Editprofile/{operatorId}', 'Auth\AdministratorLoginController@EditProfile');

Route::get('/Admin/EditDriver/{driverId}', 'Auth\AdministratorLoginController@EditDriverProfile');

Route::get('/Admin/PayOperator/{paymentId}', 'Auth\AdministratorLoginController@PayOperator');

Route::get('/Account', 'Auth\OperatorLoginController@Account');

Route::get('/Payments', 'Auth\OperatorLoginController@OperatorAccountDasboard');

Route::get('/Operator/PaymentHistory', 'Auth\OperatorLoginController@OperatorAccountHistory');

Route::post('/Operator/UpdateAccount', 'Auth\OperatorLoginController@OperatorAccountUpdate');

Route::get('/Operator/UpdatePayment', 'Auth\OperatorLoginController@UpdateOperatorAccount');

Route::get( '/Operator/Trips', 'Auth\OperatorLoginController@OperatorTrips');

Route::get('/ForgetPassword', 'Auth\AuthenticationController@ForgetPassword');

Route::post('/Login', 'Auth\LoginController@operatorweb');

Route::post('/registerOperator', 'Auth\OperatorLoginController@RegisterOperator');

Route::post('/registerbike', 'Auth\OperatorLoginController@RegisterBike');

Route::post('/registerdriver', 'Auth\OperatorLoginController@DriverRegistration');

Route::get('/registration', 'Auth\AuthenticationController@Register');

Route::get('/Administrator', 'Auth\AdministratorLoginController@Login');

Route::post('/Administrator', 'Auth\LoginController@AdminLogin');

Route::post( '/Admin/transferOtp', 'Auth\AdministratorLoginController@OtpTransfer');

Route::get('/Admin/Dashboard', 'Auth\AdministratorLoginController@Dashboard');

Route::get('/Admin/Operators', 'Auth\AdministratorLoginController@Operators');

Route::get('/Admin/Drivers', 'Auth\AdministratorLoginController@Drivers');

Route::get( '/Admin/Account', 'Auth\AdministratorLoginController@AdminProfile');

Route::get('/Admin/Trips', 'Auth\AdministratorLoginController@Trips');

Route::get('/Admin/Users', 'Auth\AdministratorLoginController@Users');

Route::get('/Admin/bikes', 'Auth\AdministratorLoginController@Bikes');

Route::get('/Admin/Account', 'Auth\AccountController@Index');

Route::get('/Admin/AccountPaymentHistory', 'Auth\AccountController@PaymentHistory');

Route::get('/Admin/AccountPendingPayment', 'Auth\AccountController@PendingPayment');

Route::get('/Admin/Operator/Driver/{operatorId}', 'Auth\AdministratorLoginController@DriverList');

Route::get( '/Admin/Operator/Bike/{operatorId}', 'Auth\AdministratorLoginController@BikeList');

Route::get('/Admin/Operator/Trip/{driverId}', 'Auth\AdministratorLoginController@TripList');

Route::get('/Admin/Operator/ActivateDriver/{driverId}', 'Auth\AdministratorLoginController@ActivateDriver');

Route::get('/Admin/Operator/SuspendDriver/{driverId}', 'Auth\AdministratorLoginController@SuspendDriver');

Route::get('/Admin/Operator/ActivateOperator/{driverId}', 'Auth\AdministratorLoginController@ActivateOperator');

Route::get('/Admin/Operator/SuspendOperator/{driverId}', 'Auth\AdministratorLoginController@SuspendOperator');

Route::get('/logout', 'Auth\LoginController@logout');

Route::get('/adminlogout', 'Auth\LoginController@Adminlogout');

Route::post('/adminpasswordreset', 'Auth\LoginController@passwordreset');

Route::get('/adminpasswordreset', 'Auth\AdministratorLoginController@AdminReset');

Route::post('/adminoperatorupdate', 'Auth\AdministratorLoginController@UpdateOperatorDetails');

Route::post('/admindriverupdate', 'Auth\AdministratorLoginController@UpdateDriverDetails');