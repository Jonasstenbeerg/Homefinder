import ObjectsNavItem from './ObjectsNavItem';
import { faRectangleAd, faUsers, faChevronRight, faChevronLeft }  from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import styles from './ObjectsNav.module.css'
import { useState } from 'react';

const ObjectsNav = ({selctedNavITem, onSelectNavItem}) => {
  const [mobileObjectsNavVisible, setMobileObjectsNavVisible] = useState(false)

  const handleToggleObjectsNav = () => {
    setMobileObjectsNavVisible(!mobileObjectsNavVisible)
  }

  return (
    <section className={`home-sidebar-container ${mobileObjectsNavVisible ? "": styles["hidden"]}`} id={styles["left-sidebar"]}>
      <h1 className="home-sidebar-heading">Objects</h1>
      <ul className={styles["objects-nav-list"]}>
        <li onClick={() => {onSelectNavItem("advertisements")}}>
          <ObjectsNavItem picture={faRectangleAd} name={'advertisements'} selected={selctedNavITem === "advertisements"}/>
        </li>
        <li onClick={() => {onSelectNavItem("users")}}>
          <ObjectsNavItem picture={faUsers} name={"users"} selected={selctedNavITem === "users"}/>
        </li>
      </ul>
      <FontAwesomeIcon onClick={handleToggleObjectsNav} icon={mobileObjectsNavVisible ? faChevronLeft:faChevronRight} className={styles["left-sidebar-toggle-button"]}/>
    </section>
  )
}
export default ObjectsNav;