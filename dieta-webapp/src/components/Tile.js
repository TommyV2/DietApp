import React from "react";
import {Card, Button} from "react-bootstrap";
import "./Tile.css";

export class Tile extends React.Component {
    render() {
        return (
            <Card style={{ width: '18rem' }}>
                <Card.Img variant="top" src={this.props.img_src} />
                <Card.Body>
                    <Card.Title>{this.props.title}</Card.Title>
                    <Card.Text>
                        {this.props.description}
                    </Card.Text>
                    <Button className="btn" variant="outline-success">Przejdź</Button>
                </Card.Body>
            </Card>
        );
    }
}