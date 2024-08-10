import React from 'react';
import './login.css'
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const navigate = useNavigate();

    const handleLogin = () => { 
        navigate('/');
    };

    const handleSignup = () => {
        navigate('/signup');
      };

    return (
        <div>
            <div className="title-container">
                <h1 className="welcome">Welcome To</h1>
                <h1 className="mugiwara">MiguwaraChat</h1>
            </div>

            <div className="login-container">
                <div className="login-form">
                    <h2 className="login-title">Login !</h2>
                    <h2 className="login-username-title">Username</h2>
                    <input className="login-username"></input>
                    <h2 className="login-password-title">Password</h2>
                    <input className="login-password"></input>
                    <button className="login-btn" onClick={handleLogin}> Login </button>
                    <div className="login-separator"></div>
                    <div className="sign-container">
                        <p className="sign1">Dont Have An Account?</p>
                        <p id="SignUp" className="sign2" onClick={handleSignup}>Sign Up Now!</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;