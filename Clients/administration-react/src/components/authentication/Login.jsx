const Login = () => {
  return (
    
    <form action="/login" method="post" className="form-container">
      <section className="form-props-area">
        <h1 className="form-heading">Sign in</h1>
        <label className="form-label" for="username">Username:</label>
        <input className="form-input" placeholder="Username" type="text" id="username" name="username" autocomplete="off" required/>
        
        <label className="form-label" for="password">Password:</label>
        <input className="form-input" placeholder="Password" type="password" id="password" name="password" autocomplete="off" required/>
        
        <a id="form-forgot-password">I forgot my password</a>

        <button className="form-submit-button" type="submit">Login</button>
      </section>
    </form>
   
  )
}
export default Login;