import React, { useState } from 'react';
import './signUp.css';
import img from '../../assets/img.png';
import { useNavigate } from 'react-router-dom';

function SignUp() {
  const [image, setImage] = useState(null); // Set initial state to null
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate('/login');
  };

  const handleImageChange = (event) => {
    setImage(event.target.files[0]); // Get the selected file
  };

  return (
    <div>
      <div className="title-container">
        <h1 className="welcome">Welcome To</h1>
        <h1 className="mugiwara">MiguwaraChat</h1>
      </div>

      <div className="signup-container">
        <div className="signup-form">
          <h2 className="signup-title">Sign Up!</h2>
          {image ? (
            <img src={URL.createObjectURL(image)} alt="User Selected" className="IMG" />
          ) : (
            <img src={img} alt="Signup Illustration" className="IMG" />
          )}
          <input
            className="pfp-input"
            type="file"
            onChange={handleImageChange} // Use onChange to handle file selection
          />
          <h2 className="signup-username-title">Username</h2>
          <input className="signup-username" type="text" />
          <h2 className="signup-password-title">Password</h2>
          <input className="signup-password" type="password" />
          <button className="signup-btn">Sign Up</button>
          <div className="signup-separator"></div>
          <div className="sign-container">
            <p className="login1">Already Have An Account?</p>
            <p id="login" className="login2" onClick={handleLogin}>Login Now!</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SignUp;