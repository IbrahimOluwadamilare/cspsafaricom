<?php

namespace App\Http\Controllers\Auth;

use Illuminate\Http\Request;
use App\User;
use App\cardDb;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Validator;
use Illuminate\Foundation\Auth\RegistersUsers;

use App\Mail\EmailNotification;
use Illuminate\Support\Facades\Mail;

class RegisterController extends Controller
{
    /*
    |--------------------------------------------------------------------------
    | Register Controller
    |--------------------------------------------------------------------------
    |
    | This controller handles the registration of new users as well as their
    | validation and creation. By default this controller uses a trait to
    | provide this functionality without requiring any additional code.
    |
    */

    use RegistersUsers;

    /**
     * Where to redirect users after registration.
     *
     * @var string
     */
    protected $redirectTo = '/home';

    /**
     * Create a new controller instance.
     *
     * @return void
     */
    public function __construct()
    {
        // $this->middleware('guest');
    }

    /**
     * Get a validator for an incoming registration request.
     *
     * @param  array  $data
     * @return \Illuminate\Contracts\Validation\Validator
     */
    protected function validator(array $request)
    {
        return Validator::make($data, [
            'firstName' => 'required|string|max:255',
            'email' => 'required|string|email|max:255|unique:users',
            'password' => 'required|string|min:8|confirmed',
        ]);
    }

    /**
     * Create a new user instance after a valid registration.
     *
     * @param  array  $data
     * @return \App\User
     */
    protected function create(Request $request)
    {
        $unqCustomerId = $this->generateCustomerId();
        $finduser = User::where('email', $request->email)->exists();
        $findPhoneNo = User::where('phoneNo', $request->phoneNo)->exists();

        if ($finduser == 'True' || $findPhoneNo == "True")
        {
            $data = 'Email OR Phone Number already exist. Please login to continue';
            $result = 'failed';
            return $this->resultResponse($data, $result);
        }else{
        $newTamarkUser = User::create([
            'firstName' => $request->firstName,
            'lastName' => $request->lastName,
            'email' => $request->email,
            'password' => bcrypt($request->password),
            'phoneNo' => $request->phoneNo,
            'address' => $request->address,
            'city' => $request->city,
            'state' => $request->state,
            'customerId' => 'TAM-USR'.$unqCustomerId,
        ]);

        if ($newTamarkUser) {

            // $newUserCardDetails = cardDb::create([
            //     'ccv' => $request->ccv,
            //     'cardNo' => $request->cardNo,
            //     'expiryDate' => $request->expiryDate,
            //     'customerId' => $unqCustomerId,
            //     'authorization_code' => $request->authorization_code,
            //     'reference'=>$request->reference,
            //     'status' => 'Active',
            // ]);
            
            if ( $newTamarkUser){
                $data = 'User Registration Successful';
                $result = 'success';

                $toemail = $request->email;
                $user_new = ['Username' =>$request->email, 'Password' => $request->password, 'Fullname' => $request->lastName.' '.$request->firstName, 'userType' => 'user', 'emailType' => 'registration' ];
    
                Mail::to($toemail)->send(new EmailNotification($user_new));

                return $this->resultResponse($data, $result);
            }else{
                $data = "Oops, User card registration failed, Please try again later";
                $result = 'failed';
                return $this->resultResponse($data, $result);
            }

        } else {
                $data = "Oops, User Registration Unssucessful Please try again later";
                $result = 'failed';
                return $this->resultResponse($data, $result);
        }
    }
    }
    public function updateCustomerLocation($request)
    {
        $id = $request->input('customerId');
        $customer = User::where('id', $id)->firstOrFail();
        $array = array(
            'longitude' => $request->input('longitude'),
            'latitude' => $request->input('latitude'),
        );
        $result = User::where('id', $id)->update($array);

        // $result = $trip->status = $request->input('status');

        // $result = $trip->save();
        if ($result) {
            $data = 'User Location Update Successsful';
            $result = 'success';
            return $this->resultResponse($data, $result);
        } else {
            $data = 'Oops, unable to update user location, please try again later';
            $result = 'failed';
            return $this->resultResponse($data, $result);
        }

    }

    public function generateCustomerId()
    {

        $unqCustomerId = mt_rand(10000000, 99999999); // better than rand()

        // call the same function if the businessId exists already

        $var = $this->CustomerIdExists($unqCustomerId);

        if ($var) {
            return generateCustomerId();
        }

        // otherwise, it's valid and can be used
        return $unqCustomerId;
    }

    public function CustomerIdExists($unqCustomerId)
    {
        // query the database and return a boolean
        // for instance, it might look like this in Laravel
        return User::where('customerId', $unqCustomerId)->exists();
    }

    public function resultResponse($data, $result)
    {
        $entry=array('status'=>$result,'message'=>$data);
        return $entry;
    }
}
