import React from "react";
import { connect } from "react-redux";
import {InvitationPanel} from "./InvitationPanel"

export class Invitations extends React.Component {

  render() {
    const invitations = this.props.userInvitations;
    let type = this.props.type;
    const handler = this.props.handler;

    return (

      <ul class="list-group">
        {!invitations ? (
          <h3>loading...</h3>
        ) :      
            invitations.map(function (invitation, idx) {
              if (type === "specialist") {
                return <InvitationPanel key={idx} id= {invitation.id} type="specialist" userId={invitation.customerId} userName={invitation.customerName} 
                sendDate={invitation.sendDate} invitedBy={invitation.invitedBy} handler={handler}/>
              } else {
                return <InvitationPanel key={idx} id= {invitation.id} type="customer" userId={invitation.specialistId} userName={invitation.specialistName} 
                sendDate={invitation.sendDate} invitedBy={invitation.invitedBy} handler={handler}/>
              }
            })       
        }
      </ul>
    )

  }
}

const mapStateToProps = (state) => {
  return {
    userInvitations: state.userInvitations

  };
};

Invitations = connect(mapStateToProps)(Invitations);