import ObjectsNav from "./objectsNav/ObjectsNav";
import ObjectsOverview from "./ObjectsOverview";
import ManageObject from "./ManageObject";

const Home = () => {
  return (
    <section className="home-wrapper">
      <nav>
        <ObjectsNav></ObjectsNav>
      </nav>
      <article>
        <ObjectsOverview></ObjectsOverview>
      </article>
      <article>
        <ManageObject></ManageObject>
      </article>
    </section>
  )
}
export default Home;