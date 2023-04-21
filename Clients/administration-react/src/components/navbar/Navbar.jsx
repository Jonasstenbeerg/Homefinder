import { useEffect, useState } from "react"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faHouse }  from '@fortawesome/free-solid-svg-icons'
import { useIsAuthenticated, useSignOut } from 'react-auth-kit';
import { useNavigate } from "react-router-dom";
import {useAuthUser} from 'react-auth-kit'


const Navbar = () => {
  const authInfo = useAuthUser()
  const signOut = useSignOut()
  const isAuthenticated = useIsAuthenticated()
  const [isBurgerMenuOpen, setIsBurgerMenuOpen] = useState(false)
  const [menuContent, setmenuContent] = useState()
  const navigate = useNavigate()
  
  //TODO: Generates warning in console
  useEffect(() => {
    updateMenuContent()
  },[isAuthenticated()])
  
  const updateMenuContent = () => {
    setmenuContent(isAuthenticated() ? 
    <li>
      <a onClick={handleSignOutClicked} href="/login">Sign out</a>
    </li>
    :
    <>
      <li>
        <a onClick={handleBurgerMenuToggle} href="/forgot-password">Forgot password</a>
      </li>
      <li>
        <a onClick={handleBurgerMenuToggle} href="/Login">Sign in</a>
      </li>
    </>)
  }

  const handleElipseMenuClicked = () => {

  }
  
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
          {menuContent}
        </ul>
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <h1 className="nav-heading">
          HomeFinder Admin
        </h1>
        {isAuthenticated() && (
          <>
            <button className="nav-elipse" onClick={handleElipseMenuClicked}>{authInfo().name[0]}</button>
            <ul className="nav-elipse-menu">
              {menuContent}
            </ul>
          </>
        )}
      </section>
    </nav>
  )
}
export default Navbar;