import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faHouse }  from '@fortawesome/free-solid-svg-icons'

const Navbar = () => {
  return (
    <nav id="nav">
      <section className='nav-inner-wrapper'>
        <FontAwesomeIcon icon={faHouse} size="xl"/>
        <h1 className="nav-heading">
          HomeFinder Admin
        </h1>
      </section>
    </nav>
  )
}
export default Navbar;