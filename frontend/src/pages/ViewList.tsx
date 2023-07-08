import { FC } from "react";
import { useParams  } from "react-router-dom";

export interface IViewListProps {}

export const ViewList: FC<IViewListProps> = () => {
  const { id } = useParams();
  
  return (
    <div className="content">
      <h1>{id}</h1>
      <p>Here are some helpful links</p>
    </div>
  )
}