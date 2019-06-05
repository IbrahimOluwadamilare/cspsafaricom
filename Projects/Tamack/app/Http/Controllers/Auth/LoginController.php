<?php

namespace App\Http\Controllers\Auth;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use App\Http\Controllers\Controller;
use Illuminate\Foundation\Auth\AuthenticatesUsers;
use Illuminate\Support\Facades\Session;
use App\Services\v1\DriverService;
use App\Services\v1\TripService;
use App\Services\v1\BusinessService;
use Illuminate\Contracts\Auth\Guard;
use App\User;
use App\driver;
use App\operator;
use App\vehicle;
use App\vehicleType;
use App\administrator;
use App\Mail\EmailNotification;
use Illuminate\Support\Facades\Mail;
use Auth;
use Route;
use DB;

class LoginController extends Controller
{
    /*
    |--------------------------------------------------------------------------
    | Login Controller
    |--------------------------------------------------------------------------
    |
    | This controller handles authenticating users for the application and
    | redirecting them to your home screen. The controller uses a trait
    | to conveniently provide its functionality to your applications.
    |
    */
    protected $business;
    protected $trip;
    protected $operator;

    use AuthenticatesUsers;

    /**
     * Where to redirect users after login.
     *
     * @var string
     */
    protected $redirectTo = '/dashboard';

    /**
     * Create a new controller instance.
     *
     * @return void
     */
    public function __construct(DriverService $service, TripService $tripservice, BusinessService $businessService)
    {
        $this->business = $service;
        $this->trip = $tripservice;
        $this->operator = $businessService;
        // $this->middleware('guest')->except('logout', 'Adminlogout');
    }

    public function logout()
    {
        Session::flush();
        return redirect('/Login');
    }

    public function Adminlogout()
    {
        Session::flush();
        return redirect( '/Administrator');
    }


    public function login(Request $request)
    {
        $email = $request->email;
        $pass = $request->password;
        // $this->attemptLogin($request)


        if ( Auth::guard('web')->attempt(['email' => $request->email, 'password' => $request->password])) {

            $userid = User::where('email', $email)->get();
            $variable = $this->filterUserInfo($userid);

            $data = $variable;
            $result = 'success';
            return $this->resultResponse($data, $result);
        } else {
            $data = 'Invalid Credentials';
            $result = 'failed';
            return $this->resultResponse($data, $result);
        }
    }

    public function passwordreset(Request $request)
    {
        $email = $request->email;
        $channel = $request->channel;

        if ($channel == 'admin') {
            
                $useridquery = administrator::where('email', $email)->exists();
                if($useridquery){
                    $useridquery1 = administrator::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );

                    $result = administrator::where('id', $var)->update($array);

                    if($result){
                        $notification = array(
                            'message' =>  'Password reset successful Please visit your mail for new password details',
                            'alert-type' => 'success'
                        );
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->email, 'userType' => 'admin', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        return view('administrator/login', compact('notification'));
                    }else{
                        $notification = array(
                            'message' =>  'Unable to reset password at the moment please try again later',
                            'alert-type' => 'error'
                        );
                        return view( 'administrator/passwordreset', compact('notification'));
                    }

                }else{
                    $notification = array(
                        'message' =>  'Administrator account doesnt exist, please try again with a valid administrator account',
                        'alert-type' => 'error'
                    );
                    return view( 'administrator/passwordreset', compact('notification'));
                }
            }elseif($channel == 'operator'){
                $useridquery = operator::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = operator::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = operator::where('id', $var)->update($array);
                    if ($result) {
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->companyName, 'userType' => 'operator', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword,'Fullname'=> $useridquery1[0]->companyName];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        $data = 'Password reset successful Please visit your mail for new password details';
                        $result = 'success';
                        return $this->resultOperatorResponse($data, $result);
                    } else {
                        $data = 'Password reset Unsuccessful';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
                } else {
                    $data = 'Operator account doesnt exist, please try again with a valid operator account';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
                }
            }elseif($channel == 'driver'){
                $useridquery = driver::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = driver::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = driver::where('id', $var)->update($array);
                    if ($result) {
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'driver', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        $data = 'Password reset successful Please visit your mail for new password details';
                        $result = 'success';
                        return $this->resultOperatorResponse($data, $result);
                    } else {
                        $data = 'Password reset Unsuccessful';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
                } else {
                    $data = 'Driver account doesnt exist, please try again with a valid driver account';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
                }

            }elseif($channel == 'user'){
                $useridquery = User::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = User::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = User::where('id', $var)->update($array);
                    if ($result) {
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'user', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        $data = 'Password reset successful Please visit your mail for new password details';
                        $result = 'success';
                        return $this->resultOperatorResponse($data, $result);
                    } else {
                        $data = 'Password reset Unsuccessful';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
                } else {
                    $data = 'User account doesnt exist, please try again with a valid User account';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
                }
            }elseif($channel == 'operatoradmin'){
                $useridquery = operator::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = operator::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = operator::where('id', $var)->update($array);
                    if ($result) {
                        $notification = array(
                            'message' =>  'Password reset successful. Mail sent.',
                            'alert-type' => 'success'
                        );
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->companyName, 'userType' => 'operator', 'emailType' => 'password_reset'];
                        $alloperators = $this->operator->getBusinessOperator('');
                        // $operatorlists = $alloperators['message'];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        // return redirect('home/dashboard');
                        return back()->with(compact('notification'));
                        // return view('administrator/operators', compact('notification', 'operatorlists'));
                    } else {
                        $notification = array(
                            'message' =>  'Password reset Unsuccessful',
                            'alert-type' => 'error'
                        );
                    $alloperators = $this->operator->getBusinessOperator('');
                    $operatorlists = $alloperators['message'];
                    return back()->with(compact('notification'));

                        // return view('administrator/operators', compact('notification', 'operatorlists'));
                    }
                } else {
                    $notification = array(
                        'message' =>  'Operator account doesnt exist, please try again with a valid operator account',
                        'alert-type' => 'error'
                    );
                    $alloperators = $this->operator->getBusinessOperator('');
                    $operatorlists = $alloperators['message'];
                        return back()->with(compact('notification'));
                        // return view('administrator/operators', compact('notification', 'operatorlists'));
                }
            }elseif($channel == 'driveradmin'){
                
                $useridquery = driver::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = driver::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = driver::where('id', $var)->update($array);
                    if ($result) {
                        $notification = array(
                            'message' =>  'Password reset successful Please visit your mail for new password details',
                            'alert-type' => 'success'
                        );
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'driver', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));

                        $samples =  DB::table('drivers')->join('vehicles', 'drivers.vehicleId', '=', 'vehicles.id')->get();
                        
                        return back()->with(compact('notification'));
                        // return view('administrator/drivers', compact('notification','samples'));
                    } else {
                        $notification = array(
                            'message' =>  'Password reset Unsuccessful',
                            'alert-type' => 'error'
                        );
                        $samples =  DB::table('drivers')->join('vehicles', 'drivers.vehicleId', '=', 'vehicles.id')->get();
                        return back()->with(compact('notification'));
                        // return view('administrator/drivers', compact('notification','samples'));
                    }
                } else {
                    $notification = array(
                        'message' =>  'Driver account doesnt exist, please try again with a valid driver account',
                        'alert-type' => 'error'
                    );
                    $samples =  DB::table('drivers')->join('vehicles', 'drivers.vehicleId', '=', 'vehicles.id')->get();
                    return back()->with(compact('notification'));
                        // return view('administrator/drivers', compact('notification','samples'));
                }
            }elseif($channel == 'useradmin'){
                $useridquery = User::where('email', $email)->exists();
                if ($useridquery) {
                    $useridquery1 = User::where('email', $email)->get();
                    $newPassword = $this->randomPassword();
                    $var = $useridquery1[0]->id;
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = User::where('id', $var)->update($array);
                    if ($result) {
                        $notification = array(
                            'message' =>  'Password reset successful Please visit your mail for new password details',
                            'alert-type' => 'success'
                        );
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'user', 'emailType' => 'password_reset'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        return back()->with(compact('notification'));
                        // return view('administrator/users', compact('notification'));
                    } else {
                        $notification = array(
                            'message' =>  'Password reset Unsuccessful',
                            'alert-type' => 'error'
                        );
                        return back()->with(compact('notification'));
                        // return view('administrator/users', compact('notification'));
                    }
                } else {
                    $notification = array(
                        'message' =>  'User account doesnt exist, please try again with a valid User account',
                        'alert-type' => 'error'
                    );
                    return back()->with(compact('notification'));
                    // return view('administrator/Users', compact('notification'));
                }
            }
    }

    public function updatepassword(Request $request)
    {
        $email = $request->email;
        $channel = $request->channel;
        $oldpassword = $request->oldpassword;
        $password = $request->password;

        if ($channel == 'admin') 
        {
            $useridquery = administrator::where('email', $email)->exists();
            if($useridquery){
                $useridquery1 = administrator::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $var3 = $useridquery1[0]->password;
                if (Hash::check($oldpassword,$var3)) {
                    $array = array(
                        'password' =>  bcrypt($newPassword),
                    );

                    $result = administrator::where('id', $var)->update($array);

                    if($result){
                        $notification = array(
                            'message' =>  'Password update successful Please visit your mail for new password details',
                            'alert-type' => 'success'
                        );
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->companyName, 'userType' => 'admin'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        return view('administrator/login', compact('notification'));
                    }else{
                        $notification = array(
                            'message' =>  'Unable to update password at the moment please try again later',
                            'alert-type' => 'error'
                        );
                        return view( 'administrator/updateprofile', compact('notification'));
                    }
                }else{
                    $notification = array(
                        'message' =>  'Old Password mismatch',
                        'alert-type' => 'error'
                    );
                    return view('administrator/updateprofile', compact('notification'));
                }    

            }else{
                $notification = array(
                    'message' =>  'Administrator account doesnt exist, please try again with a valid administrator account',
                    'alert-type' => 'error'
                );
                return view( 'administrator/passwordreset', compact('notification'));
            }
        }elseif($channel == 'operator')
        {
            $useridquery = operator::where('email', $email)->exists();
            if ($useridquery) 
            {
                $useridquery1 = operator::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $var3 = $useridquery1[0]->password;
                if (Hash::check($oldpassword,$var3)) 
                {
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = operator::where('id', $var)->update($array);
                    if ($result) {
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->companyName, 'userType' => 'operator', 'emailType' => 'password_change'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        $data = 'Password Update successful Please visit your mail for new password details';
                        $result = 'success';
                        return $this->resultOperatorResponse($data, $result);
                    } else {
                        $data = 'Password Update Unsuccessful';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
                }else{
                    $data = 'Old Password mismatch ';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
                }
            }else{
                $data = 'Operator account doesnt exist, please try again with a valid operator account';
                $result = 'failed';
                return $this->resultOperatorResponse($data, $result);
            }
        }elseif($channel == 'driver')
        {
            $useridquery = driver::where('email', $email)->exists();
            if ($useridquery) {
                $useridquery1 = driver::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $var3 = $useridquery1[0]->password;
                if (Hash::check($oldpassword,$var3)) 
                 {
                    $array = array(
                        'password' =>  bcrypt( $newPassword),
                    );
                    $result = driver::where('id', $var)->update($array);
                    if ($result) {
                        $toemail = $useridquery1[0]->email;
                        $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName + ' ' + $useridquery1[0]->firstName, 'userType' => 'driver', 'emailType' => 'password_change'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                        Mail::to($toemail)->send(new EmailNotification($user_new));
                        $data = 'Password Update successful Please visit your mail for new password details';
                        $result = 'success';
                        return $this->resultOperatorResponse($data, $result);
                    } else {
                        $data = 'Password Update Unsuccessful';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
                    }else{
                        $data = 'Old Password Mismatch ';
                        $result = 'failed';
                        return $this->resultOperatorResponse($data, $result);
                    }
            } else {
                $data = 'Driver account doesnt exist, please try again with a valid driver account';
                $result = 'failed';
                return $this->resultOperatorResponse($data, $result);
            }
        }elseif($channel == 'user')
        {
            $useridquery = User::where('email', $email)->exists();
            if ($useridquery) {
                $useridquery1 = User::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $var3 = $useridquery1[0]->password;
                if (Hash::check($oldpassword,$var3)) 
                 {
                $array = array(
                    'password' =>  bcrypt( $newPassword),
                );
                $result = User::where('id', $var)->update($array);
                if ($result) {
                    $toemail = $useridquery1[0]->email;
                    $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'user', 'emailType' => 'password_change'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                    Mail::to($toemail)->send(new EmailNotification($user_new));
                    $data = 'Password reset successful Please visit your mail for new password details';
                    $result = 'success';
                    return $this->resultOperatorResponse($data, $result);
                } else {
                    $data = 'Password reset Unsuccessful';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
                }
            }else{
                    $data = 'Password Mismatch';
                    $result = 'failed';
                    return $this->resultOperatorResponse($data, $result);
            }
            } else {
                $data = 'User account doesnt exist, please try again with a valid User account';
                $result = 'failed';
                return $this->resultOperatorResponse($data, $result);
            }
        }elseif($channel == 'operatoradmin'){
            $useridquery = operator::where('email', $email)->exists();
            if ($useridquery) {
                $useridquery1 = operator::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $array = array(
                    'password' =>  bcrypt( $newPassword),
                );
                $result = operator::where('id', $var)->update($array);
                if ($result) {
                    $notification = array(
                        'message' =>  'Password reset successful Please visit your mail for new password details',
                        'alert-type' => 'success'
                    );
                    $toemail = $useridquery1[0]->email;
                    $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->companyName, 'userType' => 'operator', 'emailType' => 'password_change'];
                    // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                    Mail::to($toemail)->send(new EmailNotification($user_new));
                    return view('administrator/login', compact('notification'));
                } else {
                    $notification = array(
                        'message' =>  'Password reset Unsuccessful',
                        'alert-type' => 'error'
                    );
                    return view('administrator/operators', compact('notification'));
                }
            } else {
                $notification = array(
                    'message' =>  'Operator account doesnt exist, please try again with a valid operator account',
                    'alert-type' => 'error'
                );
                return view('administrator/operators', compact('notification'));
            }
        }elseif($channel == 'driveradmin'){
            $useridquery = driver::where('email', $email)->exists();
            if ($useridquery) {
                $useridquery1 = driver::where('email', $email)->get();
                $newPassword = $password;
                $var = $useridquery1[0]->id;
                $array = array(
                    'password' =>  bcrypt( $newPassword),
                );
                $result = driver::where('id', $var)->update($array);
                if ($result) {
                    $notification = array(
                        'message' =>  'Password reset successful Please visit your mail for new password details',
                        'alert-type' => 'success'
                    );
                    $toemail = $useridquery1[0]->email;
                    $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'driver', 'emailType' => 'password_change'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                    Mail::to($toemail)->send(new EmailNotification($user_new));
                    return view('administrator/drivers', compact('notification'));
                } else {
                    $notification = array(
                        'message' =>  'Password reset Unsuccessful',
                        'alert-type' => 'error'
                    );
                    return view('administrator/drivers', compact('notification'));
                }
            } else {
                $notification = array(
                    'message' =>  'Driver account doesnt exist, please try again with a valid driver account',
                    'alert-type' => 'error'
                );
                return view('administrator/drivers', compact('notification'));
            }
        }elseif($channel == 'useradmin'){
            $useridquery = User::where('email', $email)->exists();
            if ($useridquery) {
                $useridquery1 = User::where('email', $email)->get();
                $newPassword = $this->randomPassword();
                $var = $useridquery1[0]->id;
                $array = array(
                    'password' =>  bcrypt( $newPassword),
                );
                $result = User::where('id', $var)->update($array);
                if ($result) {
                    $notification = array(
                        'message' =>  'Password reset successful Please visit your mail for new password details',
                        'alert-type' => 'success'
                    );
                    $toemail = $useridquery1[0]->email;
                    $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword, 'Fullname' => $useridquery1[0]->lastName.' '.$useridquery1[0]->firstName, 'userType' => 'user', 'emailType' => 'password_change'];
                        // $user_new = ['Username' => $useridquery1[0]->email, 'Password' => $newPassword];
                    Mail::to($toemail)->send(new EmailNotification($user_new));
                    return view('administrator/users', compact('notification'));
                } else {
                    $notification = array(
                        'message' =>  'Password reset Unsuccessful',
                        'alert-type' => 'error'
                    );
                    return view('administrator/users', compact('notification'));
                }
            } else {
                $notification = array(
                    'message' =>  'User account doesnt exist, please try again with a valid User account',
                    'alert-type' => 'error'
                );
                return view('administrator/Users', compact('notification'));
            }
        }
    }

    function randomPassword()
    {
        $alphabet = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.!@';
        $pass = array(); //remember to declare $pass as an array
        $alphaLength = strlen($alphabet) - 1; //put the length -1 in cache
          for ($i = 0; $i <= 10;  $i++)  {
            $n = rand( 0, $alphaLength);
             $pass[] =  $alphabet[$n ];
         }
        return implode($pass); //turn the array into a string
    }

    public function AdminLogin(Request $request)
    {
        $email = $request->email;
        $pass = $request->password;
        $useridquery = administrator::where('email', $email)->exists();

        if ($useridquery) {
            $useridquery1 = administrator::where('email', $email)->get();
            $var = $useridquery1[0]->password;
            if (Auth::guard('admini')->attempt(['email' => $request->email, 'password' => $request->password], $request->remember)) {
                $notification = array(
                    'message' =>  'Administrator Login Successful ',
                    'alert-type' => 'success'
                );
                $userid = administrator::where('email', $email)->get();
                return view( 'administrator/dashboard', compact( 'notification'));
            } else {
                $notification = array(
                    'message' =>  'User password Invalid',
                    'alert-type' => 'error'
                );
                return view('administrator/login',compact('notification'));
            }
        } else {
            $notification = array(
                'message' =>  'Invalid User Credentails',
                'alert-type' => 'error'
            );
            return view( 'administrator/login',compact('notification'));
        }
    }

    public function driver(Request $request)
    {
        $email = $request->email;
        $pass = $request->password; 
        $useridquery = driver::where('email', $email)->exists();


        if (!$useridquery) {
            $data = 'Invalid Credentials';
            $result = 'failed';
            return $this->resultResponse($data, $result);

        } else {
            $demo = driver::where('email', $email)->get();
            $var = $demo[0]['password'];
            $var2 = $demo[0]['status'];
            if (Hash::check($pass, $var) && $var2 != 'Suspended') {
                $userid = driver::where('email', $email)->get();
                $variable = $this->filterDriverInfo($userid);

                $data = $variable;
                $result = 'success';
                return $this->resultResponse($data, $result);
            } elseif ( Hash::check($pass, $var) && $var2 == 'Suspended')
            {
                $data = "Driver currently Suspended";
                $result = 'suspended';
                return $this->resultResponse($data, $result);
            } else{
                $data = "Invalid Credentials";
                $result = 'failed';
                return $this->resultResponse($data, $result);
            }
        }
    }

    public function operator(Request $request)
    {
        $email = $request->email;
        $pass = $request->password;
        $useridquery = operator::where('email', $email)->exists();

        if ($useridquery) {
            $useridquery1 = operator::where('email', $email)->get();
            $var = $useridquery1[0]->password;
            $var2 = $useridquery1[0]->status;
            if (Hash::check($pass, $var) && $var2 != 'Suspended') 
            {
            $userid = operator::where('email', $email)->get();
            $variable = $this->filterOperatorInfo($userid);

            $data = $variable;
            $result = 'success';
            return $this->resultResponse($data, $result);
            } elseif( Hash::check($pass, $var) && $var2 == 'Suspended') {
                $data = "Operator Suspended ";
                $result = 'Suspended';
                return $this->resultResponse($data, $result);
            }else {
                 $data = "Invalid password";
                $result = 'failed';
                return $this->resultResponse($data, $result);
            }
        } else {
            $data = 'Invalid Credentials';
            $result = 'failed';
            return $this->resultResponse($data, $result);
        }
    }

    public function operatorweb(Request $request)
    {
        $email = $request->email;
        $pass = $request->password;
        $useridquery = operator::where('email', $email)->exists();

        if ($useridquery) {
            $useridquery1 = operator::where('email', $email)->get();
            $var = $useridquery1[0]['password'];
            $var2 = $useridquery1[0]['status'];

            if (Auth::guard('operator')->attempt(['email' => $request->email, 'password' => $request->password], $request->remember)) {
                $userid = operator::where('email', $email)->get();
                if ($userid[0]['companyName'] != 'Suspended'){
                    $variable = $this->filterOperatorInfo($userid);
                        session(['operatorinfo'=>$variable]);
                        $notification = array(
                            'message' =>  'Welcome '. $userid[0]['companyName'],
                            'alert-type' => 'success'
                        );
                    return view('customer/dashboard', compact('notification'));
                }else{
                    $notification = array(
                            'message' =>  'Operator Currently Suspended',
                            'alert-type' => 'error'
                        );
                        return view('customer/login', compact('notification'));
                }
                }else{
                        $notification = array(
                            'message' =>  'Invalid User password',
                            'alert-type' => 'error'
                    );
                    return view('customer/login', compact('notification'));
            }
        }
        else 
        {
            $notification = array(
                'message' =>  'Invalid User Credentails',
                'alert-type' => 'error'
            );
            return view('customer/login', compact('notification'));
        }
    }

    public function filterUserInfo($UserIds)
    {
        $data = [];

        foreach ($UserIds as $Userid) {
            $fullname = $Userid->firstName . ' ' . $Userid->lastName;
            $entry = [
                'name' => $fullname,
                'email' => $Userid->email,
                'customerId' => $Userid->customerId
            ];
            $data[] = $entry;
        }
        return $data;
    }

    public function filterOperatorInfo($UserIds)
    {
        $data = [];

        foreach ($UserIds as $Userid) {
            $entry = [
                'companyName' => $Userid->companyName,
                'email' => $Userid->email,
                'operatorId' => $Userid->operatorId
            ];
            $data[] = $entry;
        }
        return $data;
    }

    public function filterDriverInfo($UserIds)
    {
        $data = [];

        foreach ($UserIds as $Userid) {
            $fullname = $Userid->firstName . ' ' . $Userid->lastName;
            $entry = [
                'name' =>
                $fullname,
                'email' => $Userid->email,
                'driverId' => $Userid->driverId,
                'status' => $Userid->status,
                'vehicleId' => $Userid->vehicleId 
            ];
            $data[] = $entry;
        }
        return $data;
    }

    public function resultResponse($data, $result)
    {
        $entry = array('status' => $result, 'userdata' => $data);
        return $entry;
    }
    public function resultOperatorResponse($data, $result)
    {
        $entry = array('status' => $result, 'message' => $data);
        return $entry;
    }
}
