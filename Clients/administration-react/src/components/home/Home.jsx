import { useState } from "react";
import ObjectsNav from "./objectsNav/ObjectsNav";
import ObjectsOverview from "./ObjectsOverview";
import ManageObject from "./ManageObject";



const Home = () => {
  const [selectedobject, setSelectedObject] = useState(null)
  
  return (
    <section className="home-wrapper">
      <nav>
        <ObjectsNav></ObjectsNav>
      </nav>
      <article>
        <ObjectsOverview selectObject={setSelectedObject}></ObjectsOverview>
      </article>
      <article>
        <ManageObject objectToManage={selectedobject}></ManageObject>
      </article>
    </section>
  )
}
export default Home;