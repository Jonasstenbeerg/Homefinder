import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faGear }  from '@fortawesome/free-solid-svg-icons'
const ObjectsNavItem = ({picture,name}) => {


  return (
    <section className='navItem-wrapper'>
      <FontAwesomeIcon className='navItem-gear' icon={faGear} size="2xl"/>
      <FontAwesomeIcon className='navItem-picture' icon={picture} size="2xl"/>
      <p>{name}</p>
    </section>
  )
}
export default ObjectsNavItem;