import ObjectsNavItem from './ObjectsNavItem';
import { faRectangleAd, faUsers, faChevronRight, faChevronLeft }  from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import styles from './ObjectsNav.module.css'
import { useState } from 'react';

const ObjectsNav = () => {
  const [mobileNavHidden, setMobileNavHidden] = useState(false)

  const handleToggleObjectsNav = () => {
    setMobileNavHidden(!mobileNavHidden)
  }

  return (
    <section className={`home-sidebar-container ${mobileNavHidden ? styles["hidden"]: ""}`} id={styles["left-sidebar"]}>
      <h1 className="home-sidebar-heading">Objects</h1>
      <ul className={styles["objects-nav-list"]}>
        <li>
          <ObjectsNavItem picture={faRectangleAd} name={'advertisements'}/>
        </li>
        <li>
          <ObjectsNavItem picture={faUsers} name={'users'}/>
        </li>
      </ul>
      <FontAwesomeIcon onClick={handleToggleObjectsNav} icon={mobileNavHidden ? faChevronRight:faChevronLeft} className={styles["left-sidebar-toggle-button"]}/>
    </section>
  )
}
export default ObjectsNav;