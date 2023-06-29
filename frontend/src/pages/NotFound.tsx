import { FC } from "react";
import { Link } from "react-router-dom";

export const NotFound: FC = () => {
  return (
    <div className="content">
      <h1>Oops! You seem to be lost.</h1>
      <p>Here are some helpful links</p>
      <Link to="/">Home</Link>
    </div>
  )
}