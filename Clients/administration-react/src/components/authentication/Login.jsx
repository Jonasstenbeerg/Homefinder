import { useState } from "react";
import { useSignIn } from "react-auth-kit";

const Login = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  const passwordChangeHandler = (e) => {
    setPassword(e.target.value)
  }

  const userNameChangeHandler = (e) => {
    setUserName(e.target.value)
  }

  const handleLogin = async (e) => {
    e.preventDefault()

    const url = `${process.env.REACT_APP_API_BASEURL}auth/login`
    
    const user = {
      userName: userName,
      password: password,
    };

    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(user),
    });
  }

  return (
    
    <form onSubmit={handleLogin} className="form-container">
      <section className="form-props-area">
        <h1 className="form-heading">Sign in</h1>
        <label className="form-label" htmlFor="form-username">Username:</label>
        <input value={password} className="form-input" onChange={passwordChangeHandler} placeholder="Username" type="text" id="form-username" name="username" tocomplete="off" required/>
        
        <label className="form-label" htmlFor="form-password">Password:</label>
        <input value={userName} className="form-input" onChange={userNameChangeHandler} placeholder="Password" type="password" id="form-password" name="password" tocomplete="off" required/>
        
        <a href="/forgot-password" id="form-forgot-password">I forgot my password</a>

        <button className="form-submit-button" type="submit">Login</button>
      </section>
    </form>
   
  )
}
export default Login;