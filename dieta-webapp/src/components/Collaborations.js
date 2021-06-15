import React from "react";
import { connect } from "react-redux";
import {CollaborationPanel} from "./CollaborationPanel"

export class Collaborations extends React.Component {

  render() {
    const collaborations = this.props.userCollaborations;
    let type = this.props.type;
    const handler=this.props.handler;
    return (

      <ul class="list-group">
        {!collaborations ? (
          <h3>loading...</h3>
        ) :      
            collaborations.map(function (collab, idx) {
              if (type === "specialist") {
                return <CollaborationPanel key={idx} type="specialist" id={collab.id} userId={collab.customerId} userName={collab.customerName} 
                info={collab.type === "dietician" ? (<div>klient</div>) : (<div>klient</div>)} handler={handler}/>
              } else {
                return <CollaborationPanel key={idx} type="customer" id={collab.id} userId={collab.specialistId} userName={collab.specialistName} 
                info={collab.type === "dietician" ? (<div>dietetyk</div>) : (<div>trener</div>)} handler={handler}/>
              }
            })       
        }
      </ul>
    )

  }
}

const mapStateToProps = (state) => {
  return {
    userCollaborations: state.userCollaborations

  };
};

Collaborations = connect(mapStateToProps)(Collaborations);