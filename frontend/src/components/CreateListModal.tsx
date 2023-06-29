import { FC } from "react";
import '../css/main.css';

interface CreateListModalProps {
  showModal: boolean;
  toggleModal(): void;
}


export const CreateListModal: FC<CreateListModalProps> = ({showModal, toggleModal}: CreateListModalProps) => {

  return (
    <> 
      {
        showModal ? (
          <div className="base-modal"> 
            <div className="create-list-modal-content"> 
              <button onClick={() => toggleModal()}>AHA another button. Click me to close this damn thing</button>
            </div>
          </div>
        ): null
      }
    </>
  )
}