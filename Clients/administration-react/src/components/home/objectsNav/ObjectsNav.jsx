import ObjectsNavItem from "./ObjectsNavItem";
import { faRectangleAd, faUsers }  from '@fortawesome/free-solid-svg-icons'

const ObjectsNav = () => {
  return (
    <section className="objects-nav-wrapper">
      <h1 className="objects-nav-heading">Objects</h1>
      <ul className="objects-nav-list">
        <li>
          <ObjectsNavItem picture={faRectangleAd} name={'advertisements'}/>
        </li>
        <li>
          <ObjectsNavItem picture={faUsers} name={'users'}/>
        </li>
      </ul>
    </section>
  )
}
export default ObjectsNav;