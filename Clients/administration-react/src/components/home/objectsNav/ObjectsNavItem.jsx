import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import  { faGear }  from '@fortawesome/free-solid-svg-icons'
import styles from './ObjectsNavItem.module.css'

const ObjectsNavItem = ({picture, name, selected}) => {
  return (
    <section className={`${styles["navItem-container"]} ${selected ? styles["selected"]:""}`}>
      <FontAwesomeIcon className={styles["navItem-gear"]} icon={faGear} size="2xl"/>
      <FontAwesomeIcon className={styles["navItem-picture"]} icon={picture} size="2xl"/>
      <p>{name}</p>
    </section>
  )
}
export default ObjectsNavItem;