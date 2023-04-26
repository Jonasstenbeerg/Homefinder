import ObjectsNavItem from "./ObjectsNavItem";
import { faRectangleAd, faUsers }  from '@fortawesome/free-solid-svg-icons'

const ObjectsNav = () => {
  return (
    <section className="home-sidebar-wrapper" id="left-sidebar">
      <h1 className="home-sidebar-heading">Objects</h1>
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