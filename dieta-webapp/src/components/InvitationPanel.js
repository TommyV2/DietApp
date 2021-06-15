import React from "react";
import { Row, Col } from "react-bootstrap";
import { Button } from "react-bootstrap";
import {url} from "../commons/properties";

export class InvitationPanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: this.props.id,
            userId: this.props.userId,
            userName: this.props.userName,
            sendDate: this.props.sendDate,
            key: this.props.key,
            type: this.props.type,
            invitedBy: this.props.invitedBy
        };
        //this.handleClick = this.handleClick.bind(this);
    }

    async handleInvitation(id, action){ 
        const data="";             
        var request = new XMLHttpRequest();
        request.open('POST', url+'/invitations/'+id+'?action='+action, true);
        request.setRequestHeader('Content-Type', 'text/plain; charset=UTF-8');
        request.send(data);
    }

    render() {
        let sender = false; // jezeli to ja wyslalem zaproszenie to bedzie jako true
        if (this.props.type === this.props.invitedBy) {
            sender = true;
        }
        const type = this.props.type === "customer" ? "client" : "specialist";
        return (         
            <div class="card" style={{ width: "15rem" }}>
                <div class="card-body">
                    <h5 class="card-title">{this.props.userName}</h5>
                    <p class="card-text">{this.props.sendDate}</p>                    
                    <Row>
                        {sender === true ? (
                            <Row> 
                                <Col xs>
                                    <h5 class="card-title">Oczekiwanie na odpowiedź</h5>
                                </Col>
                                <Col xs>
                                        <Button onClick={() => {this.handleInvitation(this.props.id, "delete"); this.props.handler();}} class="btn del btn-primary">Anuluj</Button>
                                </Col>
                            </Row> 
                        ) : (
                            <Row>    
                                <Col xs>
                                    <Button onClick={() => {this.handleInvitation(this.props.id, "accept"); this.props.handler();}} class="btn btn-primary">Zaakceptuj</Button>
                                </Col>
                                <Col xs>
                                    <Button className="del" onClick={() => {this.handleInvitation(this.props.id, "delete"); this.props.handler();}} class="btn del btn-primary">Odrzuć</Button>
                                </Col>
                            </Row>
                        )}
                    </Row>
                </div>
            </div>
        )
    }
}

