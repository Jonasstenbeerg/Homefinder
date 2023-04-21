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
  const [burgerContent, setBurgerContent] = useState()
  const navigate = useNavigate()
  
  useEffect(() => {
    updateBurgerMenu()
  },[isAuthenticated()])

  const handleBurgerMenuToggle = () => {
    setIsBurgerMenuOpen(!isBurgerMenuOpen)
  }

  const handleSignOutClicked = () => {
    navigate("/login")
    signOut()
  }

  const updateBurgerMenu = () => {
    const content = isAuthenticated() ? 
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
    </>

    setBurgerContent(content)
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
          {burgerContent}
        </ul>
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <h1 className="nav-heading">
          HomeFinder Admin
        </h1>
        {isAuthenticated() && (
          <button className="nav-signout" onClick={handleSignOutClicked}>{authInfo().name[0]}</button>
        )}
      </section>
    </nav>
  )
}
export default Navbar;