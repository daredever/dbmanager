import React from 'react';

class Columns extends React.Component {

    constructor(props) {
        super(props);
    }

    renderTableData() {
        return this.props.columns.map((column) => {
            return (
                <tr key={column.name}>
                    <td className="text-left">{column.name} <small>({column.type})</small></td>
                </tr>
            )
        })
    }

    render() {
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th className="text-left">Columns</th>
                </tr>
                </thead>
                <tbody>
                {this.renderTableData()}
                </tbody>
            </table>
        )
    }
}

export default Columns;
