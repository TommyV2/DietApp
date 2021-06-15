import React from "react";
import "./CollaborationPanel.css";
import { Row, Col, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { url } from "../commons/properties";
import "./Tile.css";


export class CollaborationPanel extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userId: this.props.userId,
            userName: this.props.userName,
            info: this.props.info,
            status: this.props.status,
            key: this.props.key,
            type: this.props.type,
            id: this.props.id
        };
        //this.handleClick = this.handleClick.bind(this);
    }

    async deleteCollaboration(id){  

        const data = "";          
 
        const putMethod = {
            method: 'DELETE', 
            headers: {
             'Content-type': 'text/plain; charset=UTF-8' 
            },
            body: data 
           }

        await fetch(url+'/collaborations/'+id, putMethod)
       
    }

    render() {
        return (
            <div class="card" style={{ width: "15rem" }}>
                <div class="card-body">
                    <h5 class="card-title">{this.props.userName}</h5>
                    <p class="card-text">{this.props.info}</p>
                    <Row>
                        <Col xs>
                            {this.props.type === "specialist" ? (
                                <Link params={{ collaborationId: this.props.id, userId: this.props.userId }} to={"/specialist/collaboration/" + this.props.id} class="btn btn-primary">Otwórz</Link>
                            ) : (
                                <Link params={{ collaborationId: this.props.id, userId: this.props.userId}} to={"/client/collaboration/" + this.props.id} class="btn btn-primary">Otwórz</Link>
                            )}
                        </Col>
                        <Col xs>
                        <Button className="del" onClick={() => {
                            this.deleteCollaboration(this.props.id);    
                            this.props.handler();                      
                        }} class="btn btn-primary">Usuń</Button>
                        </Col>
                    </Row>
                </div>
            </div>
        )
    }
}