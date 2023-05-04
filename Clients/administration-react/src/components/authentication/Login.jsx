import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useSignIn } from 'react-auth-kit';
import axios from 'axios';
import jwt_decode from 'jwt-decode';
import styles from './Login.module.css'

const Login = () => {
  const [userName, setUserName] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage]  = useState('')
  const login = useSignIn()
  const navigate = useNavigate()

  const passwordChangeHandler = (e) => {
    setPassword(e.target.value)
    setErrorMessage('')
  }

  const userNameChangeHandler = (e) => {
    setUserName(e.target.value)
    setErrorMessage('')
  }

  const isNotAuthorized = async (response) => {
    const token = jwt_decode(response?.data?.token)
    
    return token?.Admin !== 'true'
  };

  const handleLogin = async (e) => {
    e.preventDefault()
    let response

    const url = `${process.env.REACT_APP_API_BASEURL}auth/login`
    
    const user = {
      userName: userName,
      password: password,
    };
    
    try {
      response = await axios.post(url,user)

      if(await isNotAuthorized(response)) {
        setErrorMessage("Ej behörig att logga in")
        return
      }
      
      login({
        token: response.data.token,
        expiresIn: 2600,
        tokenType: "Bearer",
        authState: { name: user.userName }
      })
  
      navigate("/home")
    } catch (error) {
      setErrorMessage(error?.response?.status === 401 ? error.response.data : "Du kan inte logga in just nu")
      return
    }
  }
  return (
    <form onSubmit={handleLogin} className={styles["form-container"]}>
      <section className={styles["form-props-area"]}>
        <h1 className={styles["form-heading"]}>Sign in</h1>
        <label className={styles["form-label"]} htmlFor="form-username">Username:</label>
        <input value={userName} className={styles["form-input"]} onChange={userNameChangeHandler} placeholder="Username" type="text" id="form-username" name="username" tocomplete="off" required/>
        
        <label className="form-label" htmlFor="form-password">Password:</label>
        <input value={password} className={styles["form-input"]} onChange={passwordChangeHandler} placeholder="Password" type="password" id="form-password" name="password" tocomplete="off" required/>
        <span className={styles["form-error"]}>{errorMessage}</span>
        <a href="/forgot-password" id={styles["form-forgot-password"]}>I forgot my password</a>

        <button className={styles["form-submit-button"]} type="submit">Login</button>
      </section>
    </form>
  )
}
export default Login;