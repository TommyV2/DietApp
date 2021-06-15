import { Container, Row,  Alert, Col } from "react-bootstrap"

export function CustomBanner(props) {
    return (
        <Container>
            <Row className="justify-content-md-center">
                <Col xs lg="8">
                    <Alert variant="success">
                        <center>
                            <div>
                                <b>{props.text}</b>
                            </div>
                        </center>
                    </Alert>
                </Col>

            </Row>
        </Container>
    );
}