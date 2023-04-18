import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faHouse } from '@fortawesome/free-solid-svg-icons'

const Navbar = () => {
  return (
    <nav id="nav">
      <h1 className="logo-area">
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <span>
          Home
        </span>
        Finder
      </h1>
    </nav>
  )
}
export default Navbar;