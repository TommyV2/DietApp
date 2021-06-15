import React from "react";
import { Row, Col } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { url } from "../commons/properties";

export class UserPanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: this.props.id,
            userId: this.props.userId,
            userName: this.props.userName,
            userSurname: this.props.userSurname,
            userRole: this.props.userRole,
            userEmail: this.props.userEmail,
            type: this.props.type
        };
        //this.handleClick = this.handleClick.bind(this);
    }

    async sendInvitation(type, id, userId, userRole, handler) {
        let type2 = userRole // można potem zmienić z uwzględnieniem userRole 
        let invitedBy = type
        let customerId = id;
        let specialistId=userId;
        if (type === "specialist"){
            customerId = userId;
            specialistId=id;
        }
       
       
       const data = JSON.stringify({ customerId: customerId, specialistId: specialistId, type: type2, invitedBy: invitedBy});     
       console.log(data)
        var request = new XMLHttpRequest();
        request.open('POST', url + '/invitations', true);
        request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        request.send(data); 
        
        /*
        fetch(url + '/invitations', {
            method: 'POST',
            headers: {               
                'Content-Type': 'application/json; charset=UTF-8',
            },
            body: JSON.stringify({
                customerId: {customerId},
                specialistId: {specialistId},
                type: {type2},
                invitedBy: {invitedBy}
            })
        }) */  

        
    }

    render() {                                 
        let sender = false; // jezeli to ja wyslalem zaproszenie to bedzie jako true
        if (this.props.type === this.props.invitedBy) {
            sender = true;
        }
        const type = this.props.type === "customer" ? "client" : "specialist";
        return (
            <div class="card" style={{ width: "17rem" }}>
                <div class="card-body">
                    <h5 class="card-title">{this.props.userName} {this.props.userSurname}</h5>
                    <p class="card-text">{this.props.userEmail}</p>

                    <Row>
                        <Col xs>
                            <Button onClick={() => {
                                this.sendInvitation(this.props.type, this.props.id, this.props.userId, this.props.userRole, this.props.handler);
                                this.props.handler();
                            }} class="btn btn-primary">Zaproś</Button>
                        </Col>
                    </Row>

                </div>
            </div>
        )
    }
}
