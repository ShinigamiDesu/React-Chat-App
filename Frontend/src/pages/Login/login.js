import React, { useState } from 'react';
import './login.css'
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();

    const handleLogin = async () => {
        if (!username || !password) {
            alert("Please fill in all fields.");
            return;
        }

        // Create the login data object
        const loginData = {
            Username: username,
            Password: password
        };

        try {
            // Send the login request
            const response = await fetch('https://localhost:7245/api/User/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginData)
            });

            // Handle the response
            if (response.ok) {
                const data = await response.json();
                console.log('API Response:', data);
                // Store user ID and token in localStorage
                localStorage.setItem('userId', data.id);
                //localStorage.setItem('token', data.token);
                alert(`Login successful! Welcome ${data.username} (ID: ${data.id})`);
                navigate('/home'); // Redirect to the homepage
            } else {
                // Handle login failure
                const errorData = await response.json();
                alert(errorData.message || 'Login failed. Please try again.');
            }
        } catch (error) {
            console.error('Error during login:', error);
            setErrorMessage('An error occurred. Please try again.');
            console.log(errorMessage);
        }
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
                    <input
                    className="login-username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    />
                    <h2 className="login-username-title">Password</h2>
                    <input
                    className="login-password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    />
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