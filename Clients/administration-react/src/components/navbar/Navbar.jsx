import { useState } from "react"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faHouse }  from '@fortawesome/free-solid-svg-icons'
import { useIsAuthenticated, useSignOut } from 'react-auth-kit';
import { useNavigate } from "react-router-dom";


const Navbar = () => {
  const signOut = useSignOut()
  const isAuthenticated = useIsAuthenticated()
  const [isBurgerMenuOpen, setIsBurgerMenuOpen] = useState(false)
  const navigate = useNavigate()
  
  const handleBurgerMenuToggle = () => {
    setIsBurgerMenuOpen(!isBurgerMenuOpen)
  }

  const handleSignOutClicked = () => {
    navigate("/login")
    signOut()
  }

  return (
    <nav id="nav">
      <section className='nav-inner-wrapper'>
        <button aria-label="Toggle Menu" id="burger-menu" className={isBurgerMenuOpen ? "open": ""} onClick={handleBurgerMenuToggle}>
          <span></span>
          <span></span>
          <span></span>
        </button>
        <ul className={isBurgerMenuOpen ? "burger-menu-list": "burger-menu-list closed"}>
          <li>
            <a href="/forgot-password">Forgot password</a>
          </li>
          <li>
            <a href="/Login">Sign in</a>
          </li>
        </ul>
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <h1 className="nav-heading">
          HomeFinder Admin
        </h1>
        {isAuthenticated() && (
          <button className="nav-signout" onClick={handleSignOutClicked}>Sign out</button>
        )}
      </section>
    </nav>
  )
}
export default Navbar;