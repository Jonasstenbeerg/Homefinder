import ObjectsNavItem from './ObjectsNavItem';
import { faRectangleAd, faUsers }  from '@fortawesome/free-solid-svg-icons'
import styles from './ObjectsNav.module.css'

const ObjectsNav = () => {
  return (
    <section className="home-sidebar-container" id={styles["left-sidebar"]}>
      <h1 className="home-sidebar-heading">Objects</h1>
      <ul className={styles["objects-nav-list"]}>
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