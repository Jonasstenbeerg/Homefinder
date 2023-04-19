const Login = () => {
  return (
    
    <form action="/login" method="post" className="form-container">
      <section className="form-props-area">
        <h1 className="form-heading">Sign in</h1>
        <label className="form-label" for="form-username">Username:</label>
        <input className="form-input" placeholder="Username" type="text" id="form-username" name="username" autocomplete="off" required/>
        
        <label className="form-label" for="form-password">Password:</label>
        <input className="form-input" placeholder="Password" type="password" id="form-password" name="password" autocomplete="off" required/>
        
        <a href="/forgot-password" id="form-forgot-password">I forgot my password</a>

        <button className="form-submit-button" type="submit">Login</button>
      </section>
    </form>
   
  )
}
export default Login;