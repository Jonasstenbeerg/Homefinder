import { useState } from "react"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faHouse }  from '@fortawesome/free-solid-svg-icons'


const Navbar = () => {
  const [isBurgerMenuOpen, setIsBurgerMenuOpen] = useState(false)
  
  const handleBurgerMenuToggle = () => {
    setIsBurgerMenuOpen(!isBurgerMenuOpen)
  }

  return (
    <nav id="nav">
      <section className='nav-inner-wrapper'>
        <button aria-label="Toggle Menu" id="menu-btn"className={isBurgerMenuOpen ? "open": ""} onClick={handleBurgerMenuToggle}>
          <span></span>
          <span></span>
          <span></span>
        </button>
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <h1 className="nav-heading">
          HomeFinder Admin
        </h1>
      </section>
    </nav>
  )
}
export default Navbar;