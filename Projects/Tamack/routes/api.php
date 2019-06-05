<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
 */

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});
Route::group(['middleware' => 'auth:api'], function() {

    Route::post('v1/register', 'Auth\RegisterController@create');

    Route::post('v1/updatecustomerlocation', 'Auth\RegisterController@updateCustomerLocation');

    Route::resource('v1/operator', v1\BusinessController::class);

    Route::resource('v1/driver', v1\DriverController::class);

    Route::resource('v1/requestride', v1\RideRequestController::class);

    Route::resource('v1/trip', v1\TripController::class);

    Route::resource('v1/driverdirection', v1\DriverDirectionController::class);

    Route::resource('v1/rating', v1\RatingController::class);

    Route::post('v1/starttrip', 'v1\StartStopController@startTrip');

    Route::post('v1/stoptrip', 'v1\StartStopController@stopTrip');

    Route::post('v1/customerlogin', 'Auth\LoginController@login');

    Route::post('v1/driverlogin', 'Auth\LoginController@driver');

    Route::post('v1/operatorlogin', 'Auth\LoginController@operator');

    Route::post('v1/driverlast10trip', 'v1\TripController@getLast10TripDriver');

    Route::post('v1/customerlast10trip', 'v1\TripController@getCusLastTrip');

    Route::post('v1/customeractivetrips', 'v1\TripController@getCusActiveTrip');

    Route::post('v1/cancelrequest', 'v1\RideRequestController@cancelRequest');

    Route::post('v1/triprequeststatus', 'v1\RideRequestController@tripRequestStatus');

    Route::post('v1/goonline', 'v1\DriverController@goOnline');

    Route::post('v1/gooffline', 'v1\DriverController@goOffline');

    Route::post('v1/canceltrip', 'v1\TripController@cancelTrip');

    Route::resource('v1/card', v1\CardController::class);

    Route::post('v1/updatedriverstatus', 'v1\DriverController@updateDriverLocation');

    Route::post('v1/activatecard', 'v1\CardController@updateCardStatusNew');

    Route::post('v1/driverstatus', 'v1\DriverController@getDriverStatus');

    Route::post('v1/deletecard', 'v1\CardController@deleteCard');

    Route::post('v1/resetpassword', 'Auth\LoginController@passwordreset');

    Route::post('v1/updatepassword', 'Auth\LoginController@updatepassword');

    Route::get('v1/getpayment', 'v1\PaymentController@index');

    Route::post('v1/singlepayment', 'v1\PaymentController@singlepaymentdemo');

});


Route::get('v1/weeklyrun', 'v1\PaymentController@weeklyrun');
